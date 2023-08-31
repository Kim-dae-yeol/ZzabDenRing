using ZzabDenRing.Di;
using ZzabDenRing.Model;

namespace ZzabDenRing;

public class Game
{
    // todo screen display
    // todo di
    // todo database or file system
    private ScreenDisplay _display = new();


    public async void Start()
    {
        _display.Display();
        Container.GetRepository().SaveData();
    }


    public static IItem[] Items =
    {
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new EquipItem("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new EquipItem("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new EquipItem("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new EquipItem("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500)
    };

    public static Equipment Equipment = new Equipment(
    );

    public static Character C1 = new Character(
        "Character1",
        "전사",
        200,
        200,
        30,
        2,
        10,
        2_500,
        10,
        Items.ToList(),
        Equipment
    );

    public static Character C2 = new Character(
        "C2",
        "마법사",
        200,
        200,
        30,
        2,
        10,
        2_500,
        10,
        Items.ToList(),
        Equipment
    );

    public static Character C3 = new Character(
        "C3",
        "궁수",
        200,
        200,
        30,
        2,
        10,
        2_500,
        10,
        Items.ToList(),
        Equipment
    );
}