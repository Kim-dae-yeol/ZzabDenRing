namespace ZzabDenRing.Model;

public class Monster
{
    public string Name;
    public int Level;
    public int MaxHp;
    public int Hp;
    public int Atk;
    public int Def;
    public int RewardGold;
    public Item? RewardItem = null;

    public Monster(int rewardGold, int def, int atk, int hp, int maxHp, int level, string name, Item? rewardItem = null)
    {
        RewardItem = rewardItem;
        RewardGold = rewardGold;
        Def = def;
        Atk = atk;
        Hp = hp;
        MaxHp = maxHp;
        Level = level;
        Name = name;
    }
}