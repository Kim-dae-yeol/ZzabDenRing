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
    public bool isChoose;

    public Item(string name, string desc, int enhancement, ItemType type, int atk, int def, int critical, int hp,
        int price)
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
        isChoose = false;
    }

    public static Item Empty = new("", "", 0, ItemType.Nothing, 0, 0, 0, 0, 0);
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