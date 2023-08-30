namespace ZzabDenRing.Model;

public class Reward
{
    public int Gold;
    public Item[] Items;

    public Reward(int gold, Item[] items)
    {
        Gold = gold;
        Items = items;
    }
}