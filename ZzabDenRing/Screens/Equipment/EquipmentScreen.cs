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
    private const ConsoleColor SelectedSlotColor = ConsoleColor.Blue;

    private int EquipmentWidth => WidthPerSlot * EquipmentViewModel.SlotCols;
    private int EquipmentHeight => WidthPerSlot * EquipmentViewModel.SlotRows;

    public EquipmentScreen(Action popBackStack)
    {
        _popBackStack = popBackStack;
        Height = HeightPerSlot * EquipmentViewModel.SlotRows + 2;
        Width = WidthPerSlot * EquipmentViewModel.SlotCols + 2 + InventoryWidth;
        _vm = new();
    }

    protected override void DrawContent()
    {
        DrawEquipmentSlots();
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
            var item = _vm.GetEquipedItem(slot);
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
        bool isSelected = false,
        Item? item = null
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
        if (item == null)
        {
            //center aligned
            var text = $"•{slot.String()}";
            
            SetCursorPosition(
                left: CursorLeft - 1 + WidthPerSlot / 2 - text.LengthToDisplay() / 2,
                top: CursorTop + HeightPerSlot / 2 - 1
            );
            Write(text);
        }
        else
        {
        }
        // 
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
        _vm.OnCommand(command);
        return command != Command.Exit;
    }
}