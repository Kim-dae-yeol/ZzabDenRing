using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Equipment;

public class EquipmentViewModel
{
    private Character c = new Character("Kim",
        "전사",
        200,
        200,
        10,
        1,
        20,
        2500,
        0,
        Game.Items.ToList(),
        Game.Equipment
    );

    private EquipmentState _state;

    //x is zero-based 4칸
    private const int MAX_X = 3;

    //y is zero-based 장비창에서는 4칸 인벤토리에서는 10칸
    private int MAX_Y => _state.curX < 3 ? 3 : 10;

    public int curX => _state.curX;
    public int curY => _state.curY;


    public EquipmentViewModel()
    {
        // todo character from repo
        _state = new EquipmentState(
            c: c,
            0,
            0
        );
    }

    public void OnCommand(Command command)
    {
        switch (command)
        {
            case Command.MoveTop:
                if (curY > 0)
                {
                    _state = _state with { curY = curY - 1 };
                }

                break;
            case Command.MoveRight:
                if (curX < MAX_X)
                {
                    _state = _state with { curX = curX + 1 };
                }

                break;
            case Command.MoveBottom:
                if (curY < MAX_Y)
                {
                    _state = _state with { curY = curY + 1 };
                }

                break;
            case Command.MoveLeft:
                if (curX > 0)
                {
                    _state = _state with { curX = curX - 1 };
                }

                break;
            case Command.Interaction:
                //착용 또는 해제 
                break;
            case Command.Nothing:
                break;
            default:
                break;
        }
    }
}

public record EquipmentState(
    Character c,
    int curX,
    int curY);