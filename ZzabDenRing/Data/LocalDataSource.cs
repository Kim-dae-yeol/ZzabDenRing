using System.Text.Json;
using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class LocalDataSource : IDataSource
{
    private const string FileName = "ZzabDenRing.json";

    public Character[] GetCharacters()
    {
        Character[] characters;
        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);

        if (File.Exists(filePath))
        {
            using var file = File.OpenRead(filePath);
            var appData = JsonSerializer.Deserialize<AppData>(file);
            characters = appData.Characters;
            foreach (var pair in characters.Select((c, i) => new {i,c }))
            {
                {
                    var character = pair.c;
                    IItem[] useItems = appData.InventoryForUse[pair.i];
                    IItem[] equipItems = appData.InventoryForEquip[pair.i];
                    IItem[] materialItems = appData.InventoryForMaterial[pair.i];
                    character.Inventory =equipItems.ToList();
                    character.Inventory.AddRange(useItems);
                    character.Inventory.AddRange(materialItems);
                }
            }
        }
        else
        {
            characters = new Character[3];
        }
        return characters;
    }

    public async void SaveData(Character?[] characters)
    {
        var inventoryForEquip = characters
            .Select(it => it?.Inventory
                .OfType<EquipItem>()
                .ToArray()
            ).ToList();

        var inventoryForUse = characters
            .Select(it => it?.Inventory
                .OfType<UseItem>()
                .ToArray())
            .ToList();

        var inventoryForMaterial = characters
            .Select(it => it?.Inventory
                .OfType<MaterialItem>()
                .ToArray())
            .ToList();

        var saveData = new AppData(
            characters,
            inventoryForEquip,
            inventoryForMaterial,
            inventoryForUse);

        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);
        await using FileStream file = File.Exists(filePath)
            ? File.Open(filePath, FileMode.Truncate)
            : File.Open(filePath, FileMode.Create);
        
        await JsonSerializer.SerializeAsync(file, saveData);
    }
}