using System;
using System.IO;
using System.Reflection;

namespace Pihalve.Player.Persistence
{
    public static class SpecialFolder
    {
        public static string ProductLocalApplicationData
        {
            get
            {
                var appDataFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                if (!Directory.Exists(appDataFolderPath))
                {
                    throw new Exception($"AppData folder does not exist: {appDataFolderPath}");
                }

                var company = GetAssemblyAttribute<AssemblyCompanyAttribute>(x => x.Company);
                var product = GetAssemblyAttribute<AssemblyProductAttribute>(x => x.Product);

                var productAppDataFolderPath = Path.Combine(appDataFolderPath, company, product);
                if (!Directory.Exists(productAppDataFolderPath))
                {
                    Directory.CreateDirectory(productAppDataFolderPath);
                }

                return productAppDataFolderPath;
            }
        }

        private static string GetAssemblyAttribute<TAttr>(Func<TAttr, string> value) where TAttr : Attribute
        {
            var attribute = (TAttr)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(TAttr));
            return value.Invoke(attribute);
        }
    }
}
