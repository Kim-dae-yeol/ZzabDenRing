using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class LocalDataSource : IDataSource
{
    public Character[] GetCharacters()
    {
        //todo parsing data from local file or database
        var characters = new Character[Constants.CharacterSlot];

        characters[0] = new Character(
            "onDevelop..",
            "무직백수",
            2000,
            2000,
            200,
            30,
            200,
            250_000,
            100, // 스치기만 해도 치명타!
            Game.Items.ToList(),
            new Equipment()
        );

        characters[1] = new Character(
            "타락파워전사",
            "전사",
            2_002_000,
            2_002_000,
            5_000,
            200,
            3_000,
            0,
            100, // 스치기만 해도 치명타!
            Game.Items.ToList(),
            new Equipment()
        );

        characters[2] = new Character(
            "만수르짱",
            "석유맨",
            2000,
            2000,
            200,
            30,
            200,
            int.MaxValue,
            100, // 스치기만 해도 치명타!
            Game.Items.ToList(),
            new Equipment()
        );

        return characters;
    }
}