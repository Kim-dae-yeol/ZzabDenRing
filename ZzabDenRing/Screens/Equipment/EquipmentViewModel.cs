using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Equipment;

public class EquipmentViewModel
{
    private bool[][] _slotMatrix =
    {
        //장비창 ㅇ ㅇ ㅇ | 인벤토리 ㅇ x 10 rows
        new[] { false, true, true, true },
        new[] { true, true, true, true },
        new[] { true, true, true, true }
    };

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

    public static int SlotRows => 4;
    public static int SlotCols => 3;

    //x is zero-based 4칸
    private const int MaxX = 3;

    //y is zero-based 장비창에서는 4칸 인벤토리에서는 10칸
    private int MaxY => _state.curX < 3 ? 3 : 10;

    public int CurX => _state.curX;
    public int CurY => _state.curY;


    public EquipmentViewModel()
    {
        // todo character from repo
        _state = new EquipmentState(
            c: c,
            1,
            0
        );
    }

    public void OnCommand(Command command)
    {
        EquipmentState newState = _state;

        // todo 이동 예외 처리 하기 다음 칸으로 이동하도록 ...  
        switch (command)
        {
            case Command.MoveTop:
                if (CurY > 0)
                {
                    newState = _state with { curY = CurY - 1 };
                }

                break;
            case Command.MoveRight:
                if (CurX < MaxX)
                {
                    newState = _state with { curX = CurX + 1 };
                }

                break;
            case Command.MoveBottom:
                if (CurY < MaxY)
                {
                    newState = _state with { curY = CurY + 1 };
                }

                break;
            case Command.MoveLeft:
                if (CurX > 0)
                {
                    newState = _state with { curX = CurX - 1 };
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

        if (newState.curY >= SlotRows || _slotMatrix[newState.curY][newState.curX])
        {
            _state = newState;
            return;
        }

        if (CurY == 0 && CurX == 1)
        {
            // from helm to left 1 
            _state = newState with { curY = CurY + 1 };
        }
        else if (CurY == 1 && CurX == 0)
        {
            // from weapon to top
            _state = newState with { curX = CurX + 1 };
        }
        else
        {
            _state = newState;
        }
    }


    /** first is row and second is column*/
    public Tuple<int, int> GetRowAndCol(EquipmentSlot slot)
    {
        var x = slot switch
        {
            EquipmentSlot.Helm => 1,
            EquipmentSlot.Necklace => 2,
            EquipmentSlot.Weapon => 0,
            EquipmentSlot.Armor => 1,
            EquipmentSlot.SubWeapon => 2,
            EquipmentSlot.Pants => 1,
            EquipmentSlot.Ring1 => 0,
            EquipmentSlot.Ring2 => 2,
            EquipmentSlot.Boots => 1,
            _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
        };
        var y = slot switch
        {
            EquipmentSlot.Helm => 0,
            EquipmentSlot.Necklace => 0,
            EquipmentSlot.Weapon => 1,
            EquipmentSlot.Armor => 1,
            EquipmentSlot.SubWeapon => 1,
            EquipmentSlot.Pants => 1,
            EquipmentSlot.Ring1 => 2,
            EquipmentSlot.Ring2 => 2,
            EquipmentSlot.Boots => 2,
            _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
        };
        return new Tuple<int, int>(y, x);
    }

    public Item? GetEquipedItem(EquipmentSlot slot)
    {
        var equip = c.Equipment;
        return slot switch
        {
            //todo item from equip
            _ => null
        };
    }
}

public record EquipmentState(
    Character c,
    int curX,
    int curY);