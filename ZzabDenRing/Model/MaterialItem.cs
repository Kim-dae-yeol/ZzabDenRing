namespace ZzabDenRing.Model;

public class MaterialItem : IItem
{
    public string Name { get; }
    public string Desc { get; }
    public int Price { get; }
    public ItemType Type { get; }

    public ItemGrade Grade { get; }
    //빛바랜 돌 
    //다듬은 돌
    //빛나는 돌
    //찬란한 돌 
}