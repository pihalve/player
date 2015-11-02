using System.Windows.Input;

namespace Pihalve.Player.UI
{
    public static class Commands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand
            (
                "E_xit",
                "Exit",
                typeof(Commands),
                new InputGestureCollection()
                {
                    new KeyGesture(Key.F4, ModifierKeys.Alt)
                }
            );
    }
}
