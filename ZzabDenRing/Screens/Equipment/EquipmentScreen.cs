using static System.Console;

namespace ZzabDenRing.Screens.Equipment;

public class EquipmentScreen : BaseScreen
{
    private EquipmentViewModel _vm;
    private Action _popBackStack;


    public EquipmentScreen(Action popBackStack)
    {
        _popBackStack = popBackStack;
        _vm = new();
    }

    protected override void DrawContent()
    {
        SetCursorPositionToContentBlock();
        WriteLine("장비창화면입니다.");
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

        return command != Command.Exit;
    }
}