namespace ZzabDenRing.Model;

public class Equipment
{
    private Dictionary<EquipmentSlot, Item> _equipped = new();
    public IReadOnlyDictionary<EquipmentSlot, Item> Equiped => _equipped;

    public Equipment()
    {
        var slots = Enum.GetValues<EquipmentSlot>();
        foreach (var slot in slots)
        {
            _equipped[slot] = Item.Empty;
        }
    }

    public Item Equip(EquipmentSlot slot, Item item)
    {
        var unEquipped = Item.Empty;
        if (!_equipped[slot].IsEmptyItem())
        {
            unEquipped = UnEquip(slot);
            Equip(slot, item);
        }
        else
        {
            _equipped[slot] = item;
        }

        return unEquipped;
    }

    public Item UnEquip(EquipmentSlot slot)
    {
        _equipped.TryGetValue(slot, out var item);
        _equipped[slot] = Item.Empty;
        return item ?? Item.Empty;
    }

    public int AddedAtk()
    {
        return Equiped.Sum(item => item.Value.Atk);
    }

    public int AddedDef()
    {
        return Equiped.Sum(item => item.Value.Def);
    }

    public int AddedCritical()
    {
        return Equiped.Sum(item => item.Value.Critical);
    }

    public int AddedHp()
    {
        return Equiped.Sum(item => item.Value.Hp);
    }
}

public enum EquipmentSlot
{
    Helm,
    Necklace,
    Weapon,
    Armor,
    SubWeapon,
    Pants,
    Ring1,
    Ring2,
    Boots
}