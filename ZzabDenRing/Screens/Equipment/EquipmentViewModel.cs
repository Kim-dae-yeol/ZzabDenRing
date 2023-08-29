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

    private int SkipCount => _state.CurInventoryIdx >= MaxVisibleInventorySlots
        ? _state.CurInventoryIdx + 1 - MaxVisibleInventorySlots
        : 0;

    public IEnumerable<Item> VisibleItems => _state.C.Inventory
        .Skip(SkipCount)
        .Take(MaxVisibleInventorySlots);

    public static int SlotRows => 3;
    public static int SlotCols => 3;

    //x is zero-based 4칸
    private const int MaxX = 3;

    //y is zero-based 장비창에서는 4칸 인벤토리에서는 10칸
    private int MaxY => _state.CurX < SlotCols ? SlotRows - 1 : _state.C.Inventory.Count - 1;

    public int CurX => _state.CurX;
    public int CurY => _state.CurY;

    public bool IsCursorInInventorySlot => CurX >= SlotCols;
    private const int MaxVisibleInventorySlots = 19;

    public int CurInventorySlot => _state.CurInventoryIdx >= MaxVisibleInventorySlots
        ? MaxVisibleInventorySlots - 1
        : _state.CurInventoryIdx;


    public EquipmentViewModel()
    {
        // todo character from repo
        _state = new EquipmentState(
            C: c,
            CurX: 1,
            CurY: 0,
            CurInventoryIdx: 0
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
                    if (IsCursorInInventorySlot)
                    {
                        var idx = CurInventorySlot > 0 ? CurInventorySlot - 1 : 0;
                        newState = _state with { CurInventoryIdx = idx };
                    }
                    else
                    {
                        newState = _state with { CurY = CurY - 1 };
                    }
                }

                break;
            case Command.MoveRight:
                if (CurX < MaxX)
                {
                    if (CurX + 1 == MaxX)
                    {
                        newState = _state with
                        {
                            CurX = CurX + 1,
                            CurInventoryIdx = 0
                        };
                    }
                    else
                    {
                        newState = _state with { CurX = CurX + 1 };
                    }
                }

                break;
            case Command.MoveBottom:
                if (CurY < MaxY)
                {
                    if (IsCursorInInventorySlot)
                    {
                        var newInventoryIdx = _state.CurInventoryIdx >= MaxY
                            ? _state.CurInventoryIdx
                            : _state.CurInventoryIdx + 1;
                        newState = _state with { CurInventoryIdx = newInventoryIdx };
                    }
                    else
                    {
                        newState = _state with { CurY = CurY + 1 };
                    }
                }

                break;
            case Command.MoveLeft:
                if (CurX > 0)
                {
                    if (IsCursorInInventorySlot)
                    {
                        newState = _state with
                        {
                            CurX = CurX - 1,
                            CurY = 0
                        };
                    }
                    else
                    {
                        newState = _state with { CurX = CurX - 1 };
                    }
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

        if (IsCursorInInventorySlot || _slotMatrix[newState.CurY][newState.CurX])
        {
            //인벤토리로 넘어감 or 장비창이 존재하는 원소인 경우 바로 적용.
            _state = newState;
            return;
        }

        if (CurY == 0 && CurX == 1)
        {
            // from helm to left 1 
            _state = newState with { CurY = CurY + 1 };
        }
        else if (CurY == 1 && CurX == 0)
        {
            // from weapon to top
            _state = newState with { CurX = CurX + 1 };
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
    Character C,
    int CurX,
    int CurY,
    int CurInventoryIdx);