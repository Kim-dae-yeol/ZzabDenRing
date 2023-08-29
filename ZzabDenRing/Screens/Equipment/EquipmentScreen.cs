using System.Text;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Equipment;

public class EquipmentScreen : BaseScreen
{
    private EquipmentViewModel _vm;
    private Action _popBackStack;

    private const int WidthPerSlot = 20 + 2; // 2 is border
    private const int HeightPerSlot = 5 + 2; // 2 is border
    private const int InventoryWidth = 50;
    private int InventoryHeight => Height - 2;
    private const ConsoleColor SelectedSlotColor = ConsoleColor.Blue;

    private int EquipmentWidth => WidthPerSlot * EquipmentViewModel.SlotCols;
    private int EquipmentHeight => WidthPerSlot * EquipmentViewModel.SlotRows;

    public EquipmentScreen(Action popBackStack)
    {
        _popBackStack = popBackStack;
        Height = HeightPerSlot * EquipmentViewModel.SlotRows + 2;
        Width = WidthPerSlot * EquipmentViewModel.SlotCols + 2 + InventoryWidth;
        // border of window AND EquipmentBorder AND ItemCount
        _vm = new(inventorySlots: Height - 2 - 2 - 1); 
    }

    protected override void DrawContent()
    {
        DrawEquipmentSlots();
        DrawInventorySlots(_vm.VisibleItems);
    }

    private void DrawEquipmentSlots()
    {
        var equipmentSlots = Enum.GetValues<EquipmentSlot>();
        foreach (var slot in equipmentSlots)
        {
            var rowAndCol = _vm.GetRowAndCol(slot);
            var row = rowAndCol.Item1;
            var col = rowAndCol.Item2;
            var selected = row == _vm.CurY && col == _vm.CurX;
            var item = _vm.GetEquippedItem(slot);
            // var item = Game.Items[0];

            DrawEquipmentSlot(
                row: row,
                col: col,
                isSelected: selected,
                slot: slot,
                item: item);
        }
    }

    private void DrawEquipmentSlot(
        int row,
        int col,
        EquipmentSlot slot,
        Item item,
        bool isSelected = false
    )
    {
        if (isSelected)
        {
            ForegroundColor = SelectedSlotColor;
        }

        var left = col * WidthPerSlot + 1;
        var top = row * HeightPerSlot + 1;
        SetCursorPosition(left, top);
        for (int i = 0; i < HeightPerSlot; i++)
        {
            for (int j = 0; j < WidthPerSlot; j++)
            {
                if (i == 0 || i == HeightPerSlot - 1)
                {
                    if (j == 0 || j == WidthPerSlot - 1)
                    {
                        Write("+");
                    }
                    else
                    {
                        Write("-");
                    }
                }
                else if (j == 0 || j == WidthPerSlot - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(left, CursorTop);
        }

        ResetColor();
        SetCursorPosition(left + 1, top + 1); //테두리 안쪽으로 커서 이동

        if (item.IsEmptyItem())
        {
            var text = $"•{slot.String()}";
            //center aligned
            SetCursorPosition(
                left: CursorLeft - 1 + WidthPerSlot / 2 - text.LengthToDisplay() / 2,
                top: CursorTop + HeightPerSlot / 2 - 1
            );
            Write(text);
        }
        else
        {
            //todo 착용한 아이템이 존재하는 경우 출력하기
            var name = $"•{item.Name}";
            var type = $"•{item.Type.String()}";
            // var grade = $"•{}";
            var atk = $"•Atk : {item.Atk}";
            var def = $"•Def : {item.Def}";
            var cri = $"•Cri : {item.Critical}";
            var hp = $"•H p : {item.Hp}";

            var texts = new string[] { name, type, atk, def, cri, hp };
            var itemCursorLeft = CursorLeft;
            foreach (var line in texts)
            {
                SetCursorPosition(
                    left: itemCursorLeft,
                    top: CursorTop
                );
                WriteLine(line);
            }
        }
    }

    private void DrawInventorySlots(IEnumerable<Item> items)
    {
        var left = EquipmentWidth + 1;
        var top = ContentTop;
        SetCursorPosition(left, top);
        for (var i = 0; i < InventoryHeight; i++)
        {
            for (var j = 0; j < InventoryWidth; j++)
            {
                if (i == 0 || i == InventoryHeight - 1)
                {
                    if (j == 0 || j == InventoryWidth - 1)
                    {
                        Write("+");
                    }
                    else
                    {
                        Write("-");
                    }
                }
                else if (j == 0 || j == InventoryWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(left, CursorTop);
        }

        SetCursorPosition(left + 1, top + 1);
        WriteLine($"{$"{_vm.CurrentItemIdx + 1} / {_vm.TotalItemCount}",InventoryWidth - 4}");
        var idx = 0;
        foreach (var item in items)
        {
            var isSelected = _vm.IsCursorInInventorySlot && ((idx++) == _vm.CurInventorySlot);
            DrawInventoryItem(item, left, CursorTop, isSelected);
        }
    }

    private void DrawInventoryItem(Item item, int left, int top, bool isSelected)
    {
        SetCursorPosition(left + 1, top);
        if (isSelected)
        {
            ForegroundColor = SelectedSlotColor;
            Write("->");
        }
        else
        {
            Write("  ");
        }

        ResetColor();
        var blankForName = item.Name.LengthToDisplay();
        var nameBuilder = new StringBuilder(item.Name);
        if (item.Enhancement > 0)
        {
            nameBuilder.Insert(0, $" [ + {item.Enhancement} ]");
        }

        for (var i = 0; i < 10 - blankForName; i++)
        {
            nameBuilder.Append(' ');
        }

        var typeString = item.Type.String();
        var typeBuilder = new StringBuilder(typeString);
        var blankForType = typeString.LengthToDisplay();

        for (var i = 0; i < 10 - blankForType; i++)
        {
            typeBuilder.Append(' ');
        }

        var grade = "등급";
        var gradeBuilder = new StringBuilder(grade);
        var blankForGrade = grade.LengthToDisplay();
        for (var i = 0; i < 10 - blankForGrade; i++)
        {
            gradeBuilder.Append(' ');
        }

        Write($"{nameBuilder} |" +
              $" {typeBuilder} | " +
              $" {gradeBuilder}");
        WriteLine();
    }

    protected override bool ManageInput()
    {
        var key = ReadKey();

        var command = key.Key switch
        {
            ConsoleKey.UpArrow => Command.MoveTop,
            ConsoleKey.RightArrow => Command.MoveRight,
            ConsoleKey.DownArrow => Command.MoveBottom,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.Enter => Command.Interaction,
            ConsoleKey.X => Command.Exit,
            _ => Command.Nothing
        };

        if (command == Command.Exit)
        {
            _popBackStack();
        }

        _vm.OnCommand(command);
        return command != Command.Exit;
    }
}