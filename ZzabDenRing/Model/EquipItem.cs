using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;


public class EquipItem : IItem
{
    [JsonInclude]
    public int Enhancement;
    [JsonInclude]
    public int Atk;
    [JsonInclude]
    public int Def;
    [JsonInclude]
    public int Critical;
    [JsonInclude]
    public int Hp;

    public EquipItem(string name, string desc, int enhancement, ItemType type, int atk, int def, int critical, int hp,
        int price, ItemGrade grade = ItemGrade.Normal)
    {
        Name = name;
        Desc = desc;
        Enhancement = enhancement;
        Type = type;
        Atk = atk;
        Def = def;
        Critical = critical;
        Hp = hp;
        Price = price;
        Grade = grade;
    }

    public static EquipItem Empty = new("", "", 0, ItemType.Nothing, 0, 0, 0, 0, 0);

    [JsonInclude]
    public string Name { get; }
    [JsonInclude]
    public string Desc { get; }
    [JsonInclude]
    public int Price { get; }
    [JsonInclude]
    public ItemType Type { get; }
    [JsonInclude]
    public ItemGrade Grade { get; }
}

public enum ItemType
{
    Helm,
    Weapon,
    Armor,
    Necklace,
    SubWeapon,
    Pants,
    Ring,
    Boots,
    Nothing
}

public enum ItemGrade
{
    Normal,
    Rare,
    Epic,
    Legendary
}