using ZzabDenRing.Model;

namespace ZzabDenRing;

public class Game
{
    // todo screen display
    // todo di
    // todo database or file system
    private ScreenDisplay _display = new();

    public void Start()
    {
        _display.Display();
    }

    public static Item[] Items =
    {
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500),
        new Item("롱소드", "긴 검이다.", 0, ItemType.Weapon, 20, 0, 0, 0, 1500),
        new Item("나무방패", "나무로 된 방패다.", 0, ItemType.SubWeapon, 0, 10, 0, 0, 1500),
        new Item("천 갑옷", "천으로 만든 옷이다.", 0, ItemType.Armor, 0, 10, 0, 0, 1500),
        new Item("천 바지", "천으로 만든 옷이다", 0, ItemType.Pants, 0, 10, 0, 0, 1500)
    };

    public static Equipment Equipment = new Equipment(
    );
}