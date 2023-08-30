using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class Character
{
    /**
     *이름, 직업, 능력치, 레벨, 방어력, 체력, 공격력, 골드, 스킬(여유가 되면 ?), 인벤토리, 장비
     *
     */
    [JsonInclude] public string Name;

    [JsonInclude] public string Job;
    [JsonInclude] public int MaxHp;
    [JsonInclude] public int Hp;
    [JsonInclude] public int Atk;
    [JsonInclude] public int Level;
    [JsonInclude] public int Def;
    [JsonInclude] public int Gold;
    [JsonInclude] public int Critical;

    public List<IItem> Inventory;
    [JsonInclude] public Equipment Equipment;

    [JsonConstructor]
    public Character(string name,
        string job,
        int maxHp,
        int hp,
        int atk,
        int level,
        int def,
        int gold,
        int critical,
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
        Equipment = equipment;
        Inventory = new List<IItem>();
    }

    public Character(string name,
        string job,
        int maxHp,
        int hp,
        int atk,
        int level,
        int def,
        int gold,
        int critical,
        List<IItem> inventory,
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