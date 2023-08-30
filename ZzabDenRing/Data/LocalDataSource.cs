using System.Text.Json;
using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class LocalDataSource : IDataSource
{
    private const string FileName = "ZzabDenRing.json";

    public async Task<Character[]> GetCharacters()
    {
        Character[] characters;
        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);
        if (File.Exists(filePath))
        {
            using var file = File.OpenRead(filePath);
            var appData = await JsonSerializer.DeserializeAsync<AppData>(file);
            characters = appData.Characters;
        }
        else
        {
            characters = new Character[3];
        }


        //todo parsing data from local file or database
        //
        // characters[0] = new Character(
        //     "onDevelop..",
        //     "무직백수",
        //     2000,
        //     2000,
        //     200,
        //     30,
        //     200,
        //     250_000,
        //     100, // 스치기만 해도 치명타!
        //     Game.Items.ToList(),
        //     new Equipment()
        // );
        //
        // characters[1] = new Character(
        //     "타락파워전사",
        //     "전사",
        //     2_002_000,
        //     2_002_000,
        //     5_000,
        //     200,
        //     3_000,
        //     0,
        //     100, // 스치기만 해도 치명타!
        //     Game.Items.ToList(),
        //     new Equipment()
        // );
        //
        // characters[2] = new Character(
        //     "만수르짱",
        //     "석유맨",
        //     2000,
        //     2000,
        //     200,
        //     30,
        //     200,
        //     int.MaxValue,
        //     100, // 스치기만 해도 치명타!
        //     Game.Items.ToList(),
        //     new Equipment()
        // );

        return characters;
    }

    public async Task SaveData(Character[] characters)
    {
        var inventoryForEquip = characters
            .Select(it => it.Inventory
                .OfType<EquipItem>()
                .ToArray()
            ).ToList();

        var inventoryForUse = characters
            .Select(it => it.Inventory
                .OfType<UseItem>()
                .ToArray())
            .ToList();

        var inventoryForMaterial = characters
            .Select(it => it.Inventory
                .OfType<MaterialItem>()
                .ToArray())
            .ToList();

        var saveData = new AppData(characters, inventoryForEquip, inventoryForMaterial, inventoryForUse);

        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);
        await using var file = File.Open(filePath, FileMode.OpenOrCreate);
        await JsonSerializer.SerializeAsync(file, saveData);
    }
}