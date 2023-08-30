namespace ZzabDenRing.Model;

public interface IItem
{
    public string Name { get; }
    public string Desc { get; }
    public int Price { get; }
    public ItemGrade Grade { get; }
}