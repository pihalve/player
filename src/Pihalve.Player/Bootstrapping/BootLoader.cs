using Autofac;
using Pihalve.Player.Library;

namespace Pihalve.Player.Bootstrapping
{
    public static class BootLoader
    {
        public static IContainer Container;

        public static void Boot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TrackFactory>().As<ITrackFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LibraryXmlSerializer>().As<ILibrarySerializer>().InstancePerLifetimeScope();

            builder.RegisterType<MainWindow>().SingleInstance();

            Container = builder.Build();
        }
    }
}
