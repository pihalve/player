using Autofac;
using Pihalve.Player.Library;
using Pihalve.Player.Tagging;

namespace Pihalve.Player.Bootstrapping
{
    public static class BootLoader
    {
        public static IContainer Container;

        public static void Boot()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TagLibSharpId3TagReader>().As<ITagReader>().Named<ITagReader>("tagReader").InstancePerLifetimeScope();
            builder.RegisterType<FileTagReader>().As<ITagReader>().Named<ITagReader>("fallbackTagReader").InstancePerLifetimeScope();
            builder.Register(
                c => new TrackFactory(
                    c.ResolveNamed<ITagReader>("tagReader"), 
                    c.ResolveNamed<ITagReader>("fallbackTagReader")))
                .As<ITrackFactory>().InstancePerLifetimeScope();
            builder.RegisterType<LibraryXmlSerializer>().As<ILibrarySerializer>().InstancePerLifetimeScope();

            builder.RegisterType<MainWindow>().SingleInstance();

            Container = builder.Build();
        }
    }
}
