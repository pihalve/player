﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Autofac;
using Pihalve.Player.Bootstrapping;

namespace Pihalve.Player
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void App_OnStartup(object sender, StartupEventArgs e)
        {
            BootLoader.Boot();
            var mainWindow = BootLoader.Container.Resolve<MainWindow>();
            mainWindow.Show();
        }
    }
}
