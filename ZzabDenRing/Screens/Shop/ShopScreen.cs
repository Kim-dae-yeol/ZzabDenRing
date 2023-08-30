using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Shop;

public class ShopScreen : BaseScreen
{
    private Action _popBackStack;
    private ShopViewModel _vm;
    private const int ShopWidth = 60;
    private const int ShopHeight = 19; // 16rows + 3 [tab]
    private const int InventoryWidth = 60;
    private const int InventoryHeight = 19; // 16rows + 3 [tab]
    private const int TabWidth = 12;
    private const int TabHeight = 3;
    private int _totalWidth = ShopWidth + InventoryWidth + 4;

    public const int ItemRows = 13;

    private string[] _tabStrings =
    {
        "장비", "소모품", "재료", "강화"
    };

    private ShopTabs[] _shopTabs = Enum.GetValues<ShopTabs>();

    public ShopScreen(Action popBackStack)
    {
        _popBackStack = popBackStack;
        _vm = new();
        CommandLeft = 0;
        CommandTop = 0;
        Top = 2;
        ShownCommands = true;
    }

    protected override void DrawContent()
    {
        var shopLeft = Width / 2 - _totalWidth / 2 + 2; // window border : 2 
        var top = Top + 1;
        DrawShop(shopLeft, top);
        DrawInventory(shopLeft + ShopWidth, top);
    }

    protected override bool ManageInput()
    {
        var key = ReadKey().Key;
        var command = key switch
        {
            ConsoleKey.X => Command.Exit,
            ConsoleKey.UpArrow => Command.MoveTop,
            ConsoleKey.RightArrow => Command.MoveRight,
            ConsoleKey.DownArrow => Command.MoveBottom,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.Enter => Command.Interaction,
            _ => Command.Nothing
        };
        if (command == Command.Exit)
        {
            _popBackStack();
        }

        _vm.OnCommand(cmd: command);
        // exit screen when command is Exit
        return command != Command.Exit;
    }

    protected override void DrawCommands()
    {
        if (_vm.Message == null)
        {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine("Enter: 구입/판매/강화 | X 뒤로가기 |  ↑ → ↓ ← 으로 이동하세요.");
            ResetColor();
        }
        else
        {
            Beep();
            BackgroundColor = ConsoleColor.Red;
            WriteLine(_vm.Message);
            _vm.ConsumeMessage();
            ResetColor();
        }
    }

    private void DrawShop(int left, int top)
    {
        this.DrawBorder(left, top, ShopWidth, ShopHeight);
        var tabs = Enum
            .GetValues<ShopTabs>()
            .Select(it => _tabStrings[(int)it])
            .ToArray();
        DrawTabs(
            left: left, top: top + 1, texts: tabs, isShopTabs: true
        );
        SetCursorPosition(
            left: left + tabs.Length * TabWidth + 2,
            top: top + TabHeight / 2 + TabHeight % 2
        );
        Write($"{_vm.CurrentShopIdx + 1}/{_vm.TotalShopItems}");

        SetCursorPosition(left, top + TabHeight + 1);
        WriteLine("|      이름     |     부위    |    등급    |   가격  ");
        var skip = _vm.CurrentShopIdx >= ItemRows ? _vm.CurrentShopIdx : 0;

        var visibleItems = _vm.ShopItems
            .Skip(skip)
            .Take(ItemRows);

        DrawItems(left + 1, CursorTop, items: visibleItems, true);
    }

    private void DrawTabs(int left, int top, string[] texts, bool isShopTabs)
    {
        SetCursorPosition(left, top);
        for (var i = 0; i < texts.Length; i++)
        {
            var x = isShopTabs ? i : i + _shopTabs.Length;

            var isCursorOn = !_vm.IsInInventoryWindow &&
                             !_vm.IsInShopWindow &&
                             _vm.CurX == x;
            var selectedTabIdx = isShopTabs ? _vm.CurrentShopTab : _vm.CurrentInventoryTab;

            DrawTab(
                left: left + TabWidth * i + 1,
                top: top,
                text: texts[i],
                isSelectedTab: i == selectedTabIdx,
                isCursorOn: isCursorOn
            );
        }
    }

    private void DrawTab(int left, int top, string text, bool isSelectedTab, bool isCursorOn)
    {
        if (isCursorOn)
        {
            ForegroundColor = ConsoleColor.Green;
        }
        else if (isSelectedTab)
        {
            ForegroundColor = ConsoleColor.Blue;
        }

        this.DrawBorder(left, top, TabWidth, TabHeight);
        var textLeft = left + TabWidth / 2 - text.LengthToDisplay() / 2;
        SetCursorPosition(textLeft, top + TabHeight / 2);
        Write(text);
        ResetColor();
    }

    private void DrawInventory(int left, int top)
    {
        this.DrawBorder(left, top, InventoryWidth, InventoryHeight);
        var tabs = Enum
            .GetValues<InventoryTabs>()
            .Select(it => _tabStrings[(int)it])
            .ToArray();
        DrawTabs(left, top + 1, tabs, isShopTabs: false);
        SetCursorPosition(left, top + TabHeight + 1);
        WriteLine("|      이름     |     부위    |    등급    |   가격  ");
        DrawItems(left + 1, CursorTop, _vm.CurrentPageInventoryItems, false);
    }

    private void DrawItems(int left, int top, IEnumerable<Item> items, bool isShopItem)
    {
        foreach (var pair in items.Select((value, i) => new { i, value }))
        {
            var item = pair.value;
            var i = pair.i;
            var isCursorOn = false;

            if (isShopItem)
            {
                if (_vm.IsInShopWindow && _vm.CurrentShopIdx == i)
                {
                    isCursorOn = true;
                }
            }
            else
            {
                if (_vm.IsInInventoryWindow && _vm.CurrentInventoryIdx == i)
                {
                    isCursorOn = true;
                }
            }


            SetCursorPosition(left, top + pair.i);
            //todo align ~~~ 
            if (isCursorOn)
            {
                ForegroundColor = ConsoleColor.Blue;
                Write("☞");
                ResetColor();
            }
            else
            {
                Write(" ");
            }

            WriteLine($"{item.Name}" +
                      $"     {item.Type.String()}    |" +
                      $"    등급    |   {item.Price:N0}  ");
        }
    }

    internal enum ShopTabs
    {
        Equip,
        Use,
        Material,
        Enhancement
    }

    internal enum InventoryTabs
    {
        Equip,
        Use,
        Material
    }
}