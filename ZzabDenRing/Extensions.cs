using ZzabDenRing.Model;

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
}