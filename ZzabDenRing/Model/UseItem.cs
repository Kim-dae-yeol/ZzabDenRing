namespace ZzabDenRing.Model;

public class UseItem : IItem
{
    public string Name { get; }
    public string Desc { get; }
    public int Price { get; }
    public ItemType Type { get; }
    public ItemGrade Grade { get; }

    public Action<Character> Use;
}