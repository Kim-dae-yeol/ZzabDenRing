using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;
using ZzabDenRing.Screens;
using ZzabDenRing.Screens.CreateCharacter;
using ZzabDenRing.Screens.Dungeon;
using ZzabDenRing.Screens.Equipment;
using ZzabDenRing.Screens.Home;
using ZzabDenRing.Screens.Inventory;
using ZzabDenRing.Screens.Main;
using ZzabDenRing.Screens.Shop;
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
                popBackStack: () => { _backStack.Pop(); },
                navToCreateCharacter: () => { _backStack.Push(ScreenType.CrateCharacter); }
            ),
            ScreenType.Main => new MainScreen(
                navToShop: () => { _backStack.Push(ScreenType.Shop); },
                navToDungeonEntrance: () => { _backStack.Push(ScreenType.DungeonEntrance); },
                navToStatus: () => { _backStack.Push(ScreenType.Status); },
                navToEquipment: () => { _backStack.Push(ScreenType.Equipment); },
                navToInventory: () => { _backStack.Push(ScreenType.Inventory); },
                navToHome: () => { _backStack.Push(ScreenType.Home); }
            ),
            ScreenType.Shop => new ShopScreen(popBackStack: () => { _backStack.Pop(); }),
            ScreenType.Status => new StatusScreen(
                navToMain: () => { _backStack.Push(ScreenType.Main); },
                repository: Repository.GetInstance()
            ),
            ScreenType.Equipment => new EquipmentScreen(
                popBackStack: () => { _backStack.Pop(); }
            ),

            ScreenType.Inventory => new InventoryScreen(() => { _backStack.Pop(); }),
            ScreenType.DungeonEntrance => _dungeonEntranceScreen ??= new DungeonEntranceScreen(
                navToBattle: () => { _backStack.Push(ScreenType.DungeonBattle); },
                popBackStack: () => { _backStack.Pop(); }
            ),
            ScreenType.DungeonBattle => new DungeonBattleScreen(
                monsters: _dungeonEntranceScreen?.monsters ?? new(),
                navToMain: () =>
                {
                    do
                    {
                        _backStack.Pop();
                    } while (_backStack.Peek() != ScreenType.Main);
                }
            ),
            ScreenType.CrateCharacter => new CreateCharacterScreen(
                navToMain: () =>
                {
                    while (_backStack.Pop() == ScreenType.Home) ;
                    _backStack.Push(ScreenType.Main);
                },
                popBackStack: () => { _backStack.Pop(); }
            ),
            _ => throw new ArgumentOutOfRangeException(nameof(screenType), screenType, null)
        } ;

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
    CrateCharacter,
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
    Skill,
    Attack,
    Run,
    Wrong,
    SelectMonster,
    AttackMonster1,
    AttackMonster2,
    AttackMonster3,
    AttackCharacter,
    BattleStart,
    Delete
}