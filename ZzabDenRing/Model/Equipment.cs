using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class Equipment
{
    [JsonInclude] public Dictionary<EquipmentSlot, EquipItem> _equipped = new();
    public IReadOnlyDictionary<EquipmentSlot, EquipItem> Equiped => _equipped;

    public Equipment()
    {
        var slots = Enum.GetValues<EquipmentSlot>();
        foreach (var slot in slots)
        {
            _equipped[slot] = EquipItem.Empty;
        }
    }

    public EquipItem Equip(EquipmentSlot slot, EquipItem equipItem)
    {
        var unEquipped = EquipItem.Empty;
        if (!_equipped[slot].IsEmptyItem())
        {
            unEquipped = UnEquip(slot);
            Equip(slot, equipItem);
        }
        else
        {
            _equipped[slot] = equipItem;
        }

        return unEquipped;
    }

    public EquipItem UnEquip(EquipmentSlot slot)
    {
        _equipped.TryGetValue(slot, out var item);
        _equipped[slot] = EquipItem.Empty;
        return item ?? EquipItem.Empty;
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