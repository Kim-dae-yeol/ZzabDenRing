using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Shop;

public class ShopScreen : BaseScreen
{
    private Action _popBackStack;
    private ShopViewModel _vm;
    private const int ShopWidth = 70;
    private const int ShopHeight = 19; // 16rows + 3 [tab]
    private const int InventoryWidth = 70;
    private const int InventoryHeight = 19; // 16rows + 3 [tab]
    private const int TabWidth = 12;
    private const int TabHeight = 3;
    private int ShopLeft => Width / 2 - _totalWidth / 2 + 2; // window border : 2
    private int EnhancementSlotWidth => 22;
    private int EnhancementSlotHeight => 5;
    private int EnhancementSlotTop => Top + ShopHeight / 3;
    private int EnhancementSlotLeft => ShopLeft + ShopWidth / 2 - EnhancementSlotWidth / 2;
    private int _totalWidth = ShopWidth + InventoryWidth + 4;
    private object _lock = new();
    private bool _isAniamated;

    private bool IsAnimated
    {
        get
        {
            lock (_lock)
            {
                return _isAniamated;
            }
        }
        set
        {
            lock (_lock)
            {
                _isAniamated = value;
            }
        }
    }

    public const int ItemRows = 13;

    private string[] _tabStrings =
    {
        "장비", "소모품", "재료", "강화"
    };


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
        var top = Top + 1;
        DrawShop(ShopLeft, top);
        DrawInventory(ShopLeft + ShopWidth, top);
        if (IsAnimated)
        {
            AnimateEnhance();
        }
    }

    protected override bool ManageInput()
    {
        var key = ReadKey(true).Key;
        IsAnimated = false;
        var command = key switch
        {
            ConsoleKey.X => Command.Exit,
            ConsoleKey.Y => Command.Enhance,
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

        if (command == Command.Enhance && _vm.CanEnhance)
        {
            IsAnimated = true;
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
            HandleMessage(_vm.Message);
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


        if (_vm.CurrentShopTab != (int)ShopTabs.Enhancement)
        {
            Write($"{_vm.CurrentShopIdx + 1}/{_vm.TotalShopItems}");
            SetCursorPosition(left, top + TabHeight + 1);
            var skip = _vm.CurrentShopIdx >= ItemRows ? _vm.CurrentShopIdx - ItemRows : 0;
            var visibleItems = _vm.ShopItems
                .Skip(skip)
                .Take(ItemRows);
            DrawItems(left + 1, CursorTop, items: visibleItems, true);
        }
        else
        {
            DrawEnhancement();
        }
    }

    private void DrawTabs(int left, int top, string[] texts, bool isShopTabs)
    {
        SetCursorPosition(left, top);
        for (var i = 0; i < texts.Length; i++)
        {
            var x = isShopTabs ? i : i + _vm.ShopTabItems.Length;

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

        //todo 강화 중인 경우 장비탭만 출력하도록!
        DrawTabs(left, top + 1, tabs, isShopTabs: false);
        SetCursorPosition(left, top + TabHeight + 1);

        SetCursorPosition(
            left: left + tabs.Length * TabWidth + 2,
            top: top + TabHeight / 2 + TabHeight % 2
        );
        Write($"{_vm.CurrentInventoryIdx + 1}/{_vm.TotalInventoryItems}");
        SetCursorPosition(left, top + TabHeight + 1);

        DrawItems(left + 1, CursorTop, _vm.CurrentPageInventoryItems, false);
        DrawGold(left + InventoryWidth - 20, top + InventoryHeight, _vm.Gold);
    }

    private void DrawItems(int left, int top, IEnumerable<IItem> items, bool isShopItem)
    {
        var headers = new[] { "이름", "설명", "등급", "가격" };

        for (var k = 0; k < headers.Length; k++)
        {
            if (k == 1)
            {
                SetCursorPosition(left + 3 + 15 * k, CursorTop);
            }
            else
            {
                SetCursorPosition(left + 3 + 17 * k, CursorTop);
            }

            var header = headers[k];
            Write(header);
        }

        WriteLine();

        foreach (var pair in items.Select((value, i) => new { i, value }))
        {
            var item = pair.value;
            var i = pair.i;
            var isCursorOn = false;

            if (isShopItem)
            {
                if (_vm.IsInShopWindow && _vm.CurrentShopCursorIdx == i)
                {
                    isCursorOn = true;
                }
            }
            else
            {
                if (_vm.IsInInventoryWindow && _vm.CurrentInventoryCursorIdx == i)
                {
                    isCursorOn = true;
                }
            }


            SetCursorPosition(left, top + pair.i + 1);
            if (isCursorOn)
            {
                ForegroundColor = ConsoleColor.Cyan;
                Write("☞☞☞");
                ResetColor();
            }
            else
            {
                Write("   ");
            }

            ForegroundColor = item.Grade.Color();
            var name = item.Name;
            var desc = item.Desc.Length > 10 ? item.Desc.Substring(0, 8) + "..." : item.Desc;
            var grade = item.Grade.String();
            var price = item.Price.ToString("N0");

            var itemInfos = new[] { name, desc, grade, price };
            var itemInfoCursorLeft = CursorLeft;

            for (var k = 0; k < itemInfos.Length; k++)
            {
                if (k == 1)
                {
                    SetCursorPosition(itemInfoCursorLeft + 15 * k, CursorTop);
                }
                else
                {
                    SetCursorPosition(itemInfoCursorLeft + 17 * k, CursorTop);
                }

                var itemInfo = itemInfos[k];
                Write(itemInfo);
            }

            ResetColor();
        }
    }

    private void DrawGold(int left, int top, int gold)
    {
        SetCursorPosition(left, top);
        ForegroundColor = ConsoleColor.DarkYellow;
        Write($"Gold : ");
        Write($"{gold,13:N0}");
        ResetColor();
    }

    private void DrawEnhancement()
    {
        this.DrawBorder(EnhancementSlotLeft, EnhancementSlotTop, EnhancementSlotWidth, EnhancementSlotHeight);
        SetCursorPosition(EnhancementSlotLeft + 1, EnhancementSlotTop + 1);

        if (!_vm.EnhanceSlotItem.IsEmptyItem())
        {
            SetCursorPosition(EnhancementSlotLeft + 1, EnhancementSlotTop + 1);
            ForegroundColor = _vm.EnhanceSlotItem.Grade.Color();
            WriteLine(_vm.EnhanceSlotItem.Name);
            ResetColor();
            SetCursorPosition(EnhancementSlotLeft + 1, CursorTop);
            Write("강화확률 : ");
            var percent = _vm.EnhancePercent;
            if (percent < 50)
            {
                ForegroundColor = ConsoleColor.Red;
            }

            WriteLine($"{percent}%");
            ResetColor();
            SetCursorPosition(EnhancementSlotLeft + 1, CursorTop);

            ForegroundColor = ConsoleColor.Magenta;
            BackgroundColor = ConsoleColor.Gray;
            for (var i = 0; i < EnhancementSlotWidth - 2; i++)
            {
                if (i * 100 / EnhancementSlotWidth <= percent)
                {
                    Write('@');
                }
                else
                {
                    Write(' ');
                }
            }

            ResetColor();


            SetCursorPosition(EnhancementSlotLeft + 1, CursorTop + 2);
            WriteLine($"소유한 강화의 돌 : {_vm.StoneCount}");
            SetCursorPosition(EnhancementSlotLeft + 1, CursorTop);
            WriteLine($"필요한 강화의 돌 : {_vm.NeedStone}");
            SetCursorPosition(EnhancementSlotLeft + 1, CursorTop);
            WriteLine("강화하시려면 Y키를 누르세요.");
        }
        else
        {
            ForegroundColor = ConsoleColor.Cyan;
            var text = "강화";
            SetCursorPosition(
                CursorLeft + EnhancementSlotWidth / 2 - text.LengthToDisplay() / 2 - 1
                , CursorTop);
            WriteLine(text);
            ResetColor();
        }
    }

    private async void AnimateEnhance()
    {
        await foreach (var k in EmitEnhance())
        {
            SetCursorPosition(EnhancementSlotLeft 
                              + k, EnhancementSlotTop + 3);

            ForegroundColor = ConsoleColor.Green;
            BackgroundColor = ConsoleColor.Gray;

            if (k * 100 / EnhancementSlotWidth <= _vm.EnhancePercent && _vm.Success)
            {
                Write('@');
            }
            else
            {
                Write(' ');
            }
        }
    }

    private void HandleMessage(string msg)
    {
        var messageLeft = 20;
        var messageTop = Height - 10;
        this.DrawBorder(messageLeft, messageTop, 40, 3);
        SetCursorPosition(messageLeft + 1, messageTop + 1);
        Beep();
        BackgroundColor = ConsoleColor.Red;
        Write(msg);
        _vm.ConsumeMessage();
        ResetColor();
    }

    private async IAsyncEnumerable<int> EmitEnhance()
    {
        for (var i = 0; i < EnhancementSlotWidth - 2 && IsAnimated; i++)
        {
            await Task.Delay(20);
            yield return i;
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