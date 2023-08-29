using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Equipment;

public class EquipmentViewModel
{
    public EquipmentViewModel(int inventorySlots = 19)
    {
        Character c = new Character("Kim",
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
        // todo character from repo
        _state = new EquipmentState(
            Character: c,
            CurX: 1,
            CurY: 0,
            CurInventoryIdx: 0
        );

        _maxVisibleInventorySlots = inventorySlots;
    }

    private bool[][] _slotMatrix =
    {
        //장비창 ㅇ ㅇ ㅇ | 인벤토리 ㅇ x many rows
        new[] { false, true, true, true },
        new[] { true, true, true, true },
        new[] { false, true, false, true },
        new[] { true, true, true, true }
    };

    // todo 여기서 사용 못하도록 수정 


    private EquipmentState _state;

    private int SkipCount => _state.CurInventoryIdx >= _maxVisibleInventorySlots
        ? _state.CurInventoryIdx + 1 - _maxVisibleInventorySlots
        : 0;

    public IEnumerable<Item> VisibleItems => _state.Character.Inventory
        .Skip(SkipCount)
        .Take(_maxVisibleInventorySlots);

    public static int SlotRows => 4;
    public static int SlotCols => 3;

    //x is zero-based 4칸
    private const int MaxX = 3;

    //y is zero-based 장비창에서는 4칸 인벤토리에서는 10칸
    private int MaxY => _state.CurX < SlotCols ? SlotRows - 1 : _state.Character.Inventory.Count - 1;

    public int CurX => _state.CurX;
    public int CurY => _state.CurY;

    public bool IsCursorInInventorySlot => CurX >= SlotCols;

    private readonly int _maxVisibleInventorySlots;

    public int CurInventorySlot => _state.CurInventoryIdx >= _maxVisibleInventorySlots
        ? _maxVisibleInventorySlots - 1
        : _state.CurInventoryIdx;

    public int TotalItemCount => _state.Character.Inventory.Count;
    public int CurrentItemIdx => _state.CurInventoryIdx;

    public EquipmentSlot? SelectedSlot => _state.SelectedSlot;

    public void OnCommand(Command command)
    {
        if (command >= Command.MoveTop && command <= Command.MoveLeft)
        {
            ControlMovement(command);
            return;
        }

        if (command == Command.Interaction)
        {
            if (IsCursorInInventorySlot)
            {
                var item = _state.Character.Inventory[_state.CurInventoryIdx];
                if (item.IsEmptyItem()) return;
                if (SelectedSlot?.ToItemType() == item.Type) return;


                switch (item.Type)
                {
                    case ItemType.Weapon:
                        EquipItem(EquipmentSlot.Weapon, item);
                        break;
                    case ItemType.Armor:
                        EquipItem(EquipmentSlot.Armor, item);
                        break;
                    case ItemType.Necklace:
                        EquipItem(EquipmentSlot.Necklace, item);
                        break;
                    case ItemType.SubWeapon:
                        EquipItem(EquipmentSlot.SubWeapon, item);
                        break;
                    case ItemType.Pants:
                        EquipItem(EquipmentSlot.Pants, item);
                        break;
                    case ItemType.Ring:
                        var equipped = _state.Character.Equipment.Equiped;
                        if (equipped[EquipmentSlot.Ring1].IsEmptyItem() || !equipped[EquipmentSlot.Ring2].IsEmptyItem())
                        {
                            EquipItem(EquipmentSlot.Ring1, item);
                        }
                        else
                        {
                            EquipItem(EquipmentSlot.Ring2, item);
                        }


                        break;
                    case ItemType.Boots:
                        EquipItem(EquipmentSlot.Boots, item);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                // todo unEquip or( move to inventory and show only selected slot)
                var currentSlot = GetCurrentSlot(_state.CurY, _state.CurX);
                var equipped = GetEquippedItem(currentSlot);
                if (equipped.IsEmptyItem())
                {
                    // 2. Equip SelectedSlot
                    // so add selectedSlot state to viewModel
                    _state = _state with
                    {
                        SelectedSlot = currentSlot,
                        CurX = MaxX
                    };
                }
                else
                {
                    UnEquip(currentSlot);
                }
            }
        }
    }

    private void ControlMovement(Command command)
    {
        // Todo [ Refactor ]2가지 경우로 나누면 좋을듯
        // 1. 인벤토리에서 움직임
        // 2. 장비창에서 움직임
        EquipmentState newState = _state;
        switch (command)
        {
            case Command.MoveTop:
                if (IsCursorInInventorySlot)
                {
                    var idx = _state.CurInventoryIdx > 0 ? _state.CurInventoryIdx - 1 : 0;
                    newState = _state with { CurInventoryIdx = idx };
                }
                else
                {
                    if (CurY > 0)
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
                if (IsCursorInInventorySlot)
                {
                    newState = _state with
                    {
                        CurX = CurX - 1,
                        CurY = 0,
                        CurInventoryIdx = 0,
                        SelectedSlot = null
                    };
                }
                else if (CurX > 0)
                {
                    newState = _state with { CurX = CurX - 1 };
                }

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
            // from weapon to up or top
            _state = newState with { CurX = CurX + 1 };
        }
        else if (CurY == 3 && CurX == 0)
        {
            //ring1 to up
            _state = newState with { CurX = CurX + 1 };
        }
        else if (CurY == 2 && CurX == 1)
        {
            //pants to left or right
            _state = newState with { CurY = CurY + 1 };
        }
        else if (CurY == 3 && CurX == 2)
        {
            //ring2 to up
            _state = newState with { CurX = CurX - 1 };
        }
        else if (CurY == 1 && CurX == 2)
        {
            //subWeapon to down
            _state = newState with { CurX = CurX - 1 };
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
            EquipmentSlot.Pants => 2,
            EquipmentSlot.Ring1 => 3,
            EquipmentSlot.Ring2 => 3,
            EquipmentSlot.Boots => 3,
            _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
        };
        return new Tuple<int, int>(y, x);
    }

    public EquipmentSlot GetCurrentSlot(int row, int col)
    {
        return row switch
        {
            0 when col == 1 => EquipmentSlot.Helm,
            0 when col == 2 => EquipmentSlot.Necklace,
            1 when col == 0 => EquipmentSlot.Weapon,
            1 when col == 1 => EquipmentSlot.Armor,
            1 when col == 2 => EquipmentSlot.SubWeapon,
            2 when col == 0 => EquipmentSlot.Ring1,
            2 when col == 1 => EquipmentSlot.Boots,
            2 when col == 2 => EquipmentSlot.Ring2,
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    public Item GetEquippedItem(EquipmentSlot slot)
    {
        var equip = _state.Character.Equipment;
        return equip.Equiped[slot];
    }

    private void EquipItem(EquipmentSlot slot, Item item)
    {
        _state.Character.Inventory.RemoveAt(_state.CurInventoryIdx);
        var unEquipped = _state.Character.Equipment.Equip(slot: slot, item: item);
        if (!unEquipped.IsEmptyItem())
        {
            _state.Character.Inventory.Add(unEquipped);
        }
    }

    private void UnEquip(EquipmentSlot slot)
    {
        var item = _state.Character.Equipment.UnEquip(slot);
        _state.Character.Inventory.Add(item);
    }
}

public record EquipmentState(
    Character Character,
    int CurX,
    int CurY,
    int CurInventoryIdx,
    EquipmentSlot? SelectedSlot = null);