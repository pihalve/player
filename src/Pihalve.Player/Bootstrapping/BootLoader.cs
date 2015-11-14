using Autofac;
using Pihalve.Player.Library;
using Pihalve.Player.Persistence;
using Pihalve.Player.Tagging;

namespace Pihalve.Player.Bootstrapping
{
    public static class BootLoader
    {
        public static IContainer Container;

        public static void Boot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TagLibSharpTagReader>().As<ITagReader>().Named<ITagReader>("tagReader").InstancePerLifetimeScope();
            builder.RegisterType<FileTagReader>().As<ITagReader>().Named<ITagReader>("fallbackTagReader").InstancePerLifetimeScope();
            builder.Register(
                c => new TrackFactory(
                    c.ResolveNamed<ITagReader>("tagReader"), 
                    c.ResolveNamed<ITagReader>("fallbackTagReader")))
                .As<ITrackFactory>().InstancePerLifetimeScope();
            builder.RegisterType<AppDataXmlPersister<Library.Model.Library>>().As<IAppDataPersister<Library.Model.Library>>()
                .WithParameter("productAppDataFolderPath", SpecialFolder.ProductLocalApplicationData).InstancePerLifetimeScope();

            builder.RegisterType<MainWindow>().SingleInstance();

            Container = builder.Build();
        }
    }
}
