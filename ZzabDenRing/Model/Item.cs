namespace ZzabDenRing.Model;

public class Item
{
    public string Name;
    public string Desc;
    public int Enhancement;
    public ItemType Type;
    public int Atk;
    public int Def;
    public int Critical;
    public int Hp;
    public int Price;

    public Item(string name, string desc, int enhancement, ItemType type, int atk, int def, int critical, int hp, int price)
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
    }
}

public enum ItemType
{
    Weapon,
    Armor,
    Necklace,
    SubWeapon,
    Pants,
    Ring,
    Boots
}