namespace ZzabDenRing.Model;

public class Reward
{
    public int Gold;
    public IItem[] Items;

    public Reward(int gold, IItem[] items)
    {
        Gold = gold;
        Items = items;
    }
}