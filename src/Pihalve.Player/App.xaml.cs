using System.Windows;
using Autofac;
using Pihalve.Player.Bootstrapping;
using Pihalve.Player.Properties;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            if (!Settings.Default.Upgraded)
            {
                Settings.Default.Upgrade();
                Settings.Default.Upgraded = true;
                Settings.Default.Save();
            }

            BootLoader.Boot();

            var mainWindow = BootLoader.Resolve<MainWindow>();
            mainWindow.Show();
        }

        private void App_OnExit(object sender, ExitEventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
