using ZzabDenRing.Model;
using ZzabDenRing.Screens;

namespace ZzabDenRing;

public static class Extensions
{
    public static bool IsHanGul(this char c) => (c >= 4352 && c <= 4601) ||
                                                (c >= 44032 && c <= 55203) ||
                                                (c >= 12593 && c <= 12686);

    public static int UnicodeCount(this string s) => s.Count(c => c.IsHanGul());

    public static int LengthToDisplay(this string s) => s.Length + s.UnicodeCount();

    public static string String(this EquipmentSlot s)
    {
        return s switch
        {
            EquipmentSlot.Helm => "투구",
            EquipmentSlot.Necklace => "목걸이",
            EquipmentSlot.Weapon => "무기",
            EquipmentSlot.Armor => "갑옷",
            EquipmentSlot.SubWeapon => "보조무기",
            EquipmentSlot.Pants => "바지",
            EquipmentSlot.Ring1 => "반지1",
            EquipmentSlot.Ring2 => "반지2",
            EquipmentSlot.Boots => "신발",
            _ => ""
        };
    }

    public static string String(this ItemType t)
    {
        return t switch
        {
            ItemType.Helm => "투구",
            ItemType.Weapon => "무기",
            ItemType.Armor => "갑옷",
            ItemType.Necklace => "목걸이",
            ItemType.SubWeapon => "보조무기",
            ItemType.Pants => "바지",
            ItemType.Ring => "반지",
            ItemType.Boots => "신발",
            _ => ""
        };
    }

    public static bool IsEmptyItem(this Item i) => string.IsNullOrEmpty(i.Name);

    public static ItemType ToItemType(this EquipmentSlot slot) => slot switch
    {
        EquipmentSlot.Helm => ItemType.Armor,
        EquipmentSlot.Necklace => ItemType.Necklace,
        EquipmentSlot.Weapon => ItemType.Weapon,
        EquipmentSlot.Armor => ItemType.Armor,
        EquipmentSlot.SubWeapon => ItemType.SubWeapon,
        EquipmentSlot.Pants => ItemType.Pants,
        EquipmentSlot.Ring1 => ItemType.Ring,
        EquipmentSlot.Ring2 => ItemType.Ring,
        EquipmentSlot.Boots => ItemType.Boots,
        _ => throw new ArgumentOutOfRangeException(nameof(slot), slot, null)
    };

    public static String ToEmoji(this ItemType t) => t switch
    {
        ItemType.Helm => "🎩",
        ItemType.Weapon => "🗡️",
        ItemType.Armor => "🥋",
        ItemType.Necklace => "📿",
        ItemType.SubWeapon => "🛡️",
        ItemType.Pants => "👖",
        ItemType.Ring => "💍",
        ItemType.Boots => "🥾",
        ItemType.Nothing => "",
        _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
    };

    public static void DrawBorder(this BaseScreen s,int left, int top, int width, int height)
    {
        Console.SetCursorPosition(left, top);
        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                if (i == 0 || i == height - 1)
                {
                    if (j == 0 || j == width - 1)
                    {
                        Console.Write("+");
                    }
                    else
                    {
                        Console.Write("-");
                    }
                }else if (j == 0 || j == width - 1)
                {
                    Console.Write("|");
                }
                else
                {
                    Console.Write(" ");
                }
                
            }

            Console.WriteLine();
            Console.SetCursorPosition(left, Console.CursorTop);
        }
    }
}