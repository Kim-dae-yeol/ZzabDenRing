namespace ZzabDenRing.Model;

public class EquipItem : IItem
{
    private string _name;
    private string _desc;
    private int _price;
    private ItemGrade _grade;

    public int Enhancement;
    private ItemType _type;
    public int Atk;
    public int Def;
    public int Critical;
    public int Hp;

    public EquipItem(string name, string desc, int enhancement, ItemType type, int atk, int def, int critical, int hp,
        int price, ItemGrade grade = ItemGrade.Normal)
    {
        _name = name;
        _desc = desc;
        Enhancement = enhancement;
        _type = type;
        Atk = atk;
        Def = def;
        Critical = critical;
        Hp = hp;
        _price = price;
        _grade = grade;
    }

    public static EquipItem Empty = new("", "", 0, ItemType.Nothing, 0, 0, 0, 0, 0);
    public string Name => _name;
    public string Desc => _desc;
    public int Price => _price;
    public ItemType Type => _type;
    public ItemGrade Grade => _grade;
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