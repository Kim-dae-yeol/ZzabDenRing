namespace ZzabDenRing;

public static class Extensions
{
    public static bool IsHanGul(this char c) => (c >= 4352 && c <= 4601) ||
                                                (c >= 44032 && c <= 55203) ||
                                                (c >= 12593 && c <= 12686);

    public static int UnicodeCount(this string s) => s.Count(c => c.IsHanGul());

    public static int LengthToDisplay(this string s) => s.Length + s.UnicodeCount();
}