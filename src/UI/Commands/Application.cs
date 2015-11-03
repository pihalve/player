using System.Windows.Input;

namespace Pihalve.Player.UI.Commands
{
    public static class Application
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "E_xit",
                "Exit",
                typeof(Application),
                new InputGestureCollection
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );
    }
}
