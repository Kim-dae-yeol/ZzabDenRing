namespace ZzabDenRing.Model;

public class Character
{
    /**
     *이름, 직업, 능력치, 레벨, 방어력, 체력, 공격력, 골드, 스킬(여유가 되면 ?), 인벤토리, 장비
     *
     */
    public string Name;

    public string Job;
    public int MaxHp;
    public int Hp;
    public int Atk;
    public int Level;
    public int Def;
    public int Gold;
    public int Critical;

    public List<EquipItem> Inventory;
    public Equipment Equipment;

    public Character(string name,
        string job,
        int maxHp,
        int hp,
        int atk,
        int level,
        int def,
        int gold,
        int critical,
        List<EquipItem> inventory,
        Equipment equipment)
    {
        Name = name;
        Job = job;
        MaxHp = maxHp;
        Hp = hp;
        Atk = atk;
        Level = level;
        Def = def;
        Gold = gold;
        Critical = critical;
        Inventory = inventory;
        Equipment = equipment;
    }

    public bool IsCritical()
    {
        // 계산 공식 1 ~ 100 숫자중에서 Critical 보다 값이 낮으면 현재 공격은 크리티컬이다.
        return Random.Shared.Next(1, 101) <= Critical;
    }

    // todo skill 추가
}