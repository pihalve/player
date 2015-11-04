using System.Windows.Input;

namespace Pihalve.Player.Commands
{
    public static class Playback
    {
        public static readonly RoutedUICommand Play = new RoutedUICommand
            (
                "_Play",
                "Play",
                typeof(Application),
                new InputGestureCollection()
                {
                }
            );

        public static readonly RoutedUICommand Pause = new RoutedUICommand
            (
                "_Pause",
                "Pause",
                typeof(Application),
                new InputGestureCollection()
                {
                }
            );

        public static readonly RoutedUICommand Stop = new RoutedUICommand
            (
                "_Stop",
                "Stop",
                typeof(Application),
                new InputGestureCollection()
                {
                }
            );

        public static readonly RoutedUICommand Previous = new RoutedUICommand
            (
                "P_revious",
                "Previous",
                typeof(Application),
                new InputGestureCollection()
                {
                }
            );

        public static readonly RoutedUICommand Next = new RoutedUICommand
            (
                "_Next",
                "Next",
                typeof(Application),
                new InputGestureCollection()
                {
                }
            );
    }
}
