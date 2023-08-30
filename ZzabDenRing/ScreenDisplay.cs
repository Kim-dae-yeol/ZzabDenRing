using ZzabDenRing.Screens;
using ZzabDenRing.Screens.Dungeon;
using ZzabDenRing.Screens.Equipment;
using ZzabDenRing.Screens.Home;
using ZzabDenRing.Screens.Main;
using ZzabDenRing.Screens.Status;

namespace ZzabDenRing;

public class ScreenDisplay
{
    public Dictionary<string, object> NavArgs = new(10);
    private Stack<ScreenType> _backStack = new(10);
    internal IReadOnlyCollection<ScreenType> BackStack => _backStack;

    private DungeonEntranceScreen? _dungeonEntranceScreen = null;

    public ScreenDisplay()
    {
        _backStack.Push(ScreenType.Home);
    }

    private void DisplayScreen(ScreenType screenType)
    {
        IScreen screen = screenType switch
        {
            ScreenType.Home => new HomeScreen(
                navToMain: () => { _backStack.Push(ScreenType.Main); },
                popBackStack: () => { _backStack.Pop(); }
            ),
            ScreenType.Main => new MainScreen(
                popBackStack: () => { _backStack.Pop(); },
                navToShop: () => { },
                navToDungeonEntrance: () => { _backStack.Push(ScreenType.DungeonEntrance); },
                navToStatus: () => { _backStack.Push(ScreenType.Status); },
                navToEquipment: () => { _backStack.Push(ScreenType.Equipment); }
            ),
            ScreenType.Shop => throw new NotImplementedException(),
            ScreenType.Status => new StatusScreen(),
            ScreenType.Equipment => new EquipmentScreen(
                popBackStack: () => { _backStack.Pop(); }
            ),
            ScreenType.Inventory => throw new NotImplementedException(),
            ScreenType.DungeonEntrance => _dungeonEntranceScreen ??= new DungeonEntranceScreen(
                navToBattle: () => { _backStack.Push(ScreenType.DungeonBattle); },
                popBackStack: () => { _backStack.Pop(); }
            ),
            ScreenType.DungeonBattle => new DungeonBattleScreen(
                monsters: _dungeonEntranceScreen?.monsters ?? new()
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
    Main,
    Shop,
    Status,
    Equipment,
    Inventory,
    DungeonEntrance,
    DungeonBattle,
}

public enum Command
{
    Exit,
    MoveTop,
    MoveRight,
    MoveBottom,
    MoveLeft,
    Interaction,
    Nothing,
    Wrong
}