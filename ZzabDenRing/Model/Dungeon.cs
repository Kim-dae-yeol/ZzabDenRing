namespace ZzabDenRing.Model;

public class Dungeon
{
    public string Name;
    public string Desc;
    public int Level;
    public List<Monster> Monsters;

    public Dungeon(string name, string desc, int level, List<Monster> monsters)
    {
        Name = name;
        Desc = desc;
        Level = level;
        Monsters = monsters;
    }
    
    public int RewardGold()
    {
        return 0;
    }
}