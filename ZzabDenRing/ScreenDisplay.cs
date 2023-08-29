using ZzabDenRing.Screens;
using ZzabDenRing.Screens.Dungeon;
using ZzabDenRing.Screens.Home;

namespace ZzabDenRing;

public class ScreenDisplay
{
    public Dictionary<string, object> NavArgs = new(10);
    private Stack<ScreenType> _backStack = new(10);
    internal IReadOnlyCollection<ScreenType> BackStack => _backStack;

    public ScreenDisplay()
    {
        _backStack.Push(ScreenType.Home);
    }

    private void DisplayScreen(ScreenType screenType)
    {
        IScreen screen = screenType switch
        {
            ScreenType.Home => new HomeScreen(
                navToMain: () => { },
                popBackStack: () => { _backStack.Pop(); }
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null)
        };

        screen.DrawScreen();
    }

    public void Display()
    {
        do
        {
            var current = _backStack.Peek();
            DisplayScreen(current);
        } while (_backStack.Count > 0);
    }
}

public enum ScreenType
{
    Home,
}

public enum Command
{
    Exit,
    MoveTop,
    MoveRight,
    MoveBottom,
    MoveLeft,
    Interaction,
    Nothing
}