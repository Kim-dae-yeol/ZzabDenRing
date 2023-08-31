using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class EquipItem : IItem
{
    [JsonInclude] public int Enhancement;
    [JsonInclude] public int Atk;
    [JsonInclude] public int Def;
    [JsonInclude] public int Critical;
    [JsonInclude] public int Hp;

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

    [JsonInclude] public string Name { get; }
    [JsonInclude] public string Desc { get; }
    [JsonInclude] public int Price { get; }
    [JsonInclude] public ItemType Type { get; }
    [JsonInclude] public ItemGrade Grade { get; }

    public void Enhance()
    {
        Enhancement++;
        switch (Type)
        {
            case ItemType.Helm:
                Def += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.Weapon:
                Atk += ((int)Grade * 4 + 1) * 20;
                Critical += ((int)Grade * 4 + 1) * 2;
                break;
            case ItemType.Armor:
                Def += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.Necklace:
                Atk += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.SubWeapon:
                Def += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.Pants:
                Def += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.Ring:
                Atk += ((int)Grade * 4 + 1) * 10;
                Critical += ((int)Grade * 4 + 1) * 1;
                break;
            case ItemType.Boots:
                Def += ((int)Grade * 4 + 1) * 10;
                Hp += ((int)Grade * 4 + 1) * 50;
                break;
            case ItemType.Nothing:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public enum ItemType
{
    Helm,
    Armor,
    Weapon,
    Boots,
    Ring,
    Necklace,
    SubWeapon,
    Pants,
    Nothing
}

public enum ItemGrade
{
    Normal,
    Rare,
    Epic,
    Legendary
}