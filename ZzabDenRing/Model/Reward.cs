namespace ZzabDenRing.Model;

public class Reward
{
    public int Gold;
    public EquipItem[] Items;

    public Reward(int gold, EquipItem[] items)
    {
        Gold = gold;
        Items = items;
    }
}