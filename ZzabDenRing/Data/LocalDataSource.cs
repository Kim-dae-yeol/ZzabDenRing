using System.Text.Json;
using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class LocalDataSource : IDataSource
{
    private const string FileName = "ZzabDenRing.json";

    public Character[] GetCharacters()
    {
        Character?[] characters;
        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);

        if (File.Exists(filePath))
        {
            using var file = File.OpenRead(filePath);
            var appData = JsonSerializer.Deserialize<AppData>(file);
            characters = appData.Characters;
            foreach (var pair in characters.Select((c, i) => new { i, c }))
            {
<<<<<<< Updated upstream
                {
                    var character = pair.c;
                    if (character == null) continue;
                    IItem[] useItems = appData.InventoryForUse[pair.i];
                    IItem[] equipItems = appData.InventoryForEquip[pair.i];
                    IItem[] materialItems = appData.InventoryForMaterial[pair.i];
                    var list = new List<IItem>();
                    if (useItems != null)
                        list.AddRange(useItems);
                    if (equipItems != null)
                        list.AddRange(equipItems);
                    if (materialItems != null)
                        list.AddRange(materialItems);
                    character.Inventory = list;
                }
=======
                var character = pair.c;
                if(character == null) continue;
                IItem[] useItems = appData.InventoryForUse[pair.i];
                IItem[] equipItems = appData.InventoryForEquip[pair.i];
                IItem[] materialItems = appData.InventoryForMaterial[pair.i];

                var list = new List<IItem>();
                list.AddRange(useItems);
                list.AddRange(equipItems);
                list.AddRange(materialItems);
                character.Inventory = list;
>>>>>>> Stashed changes
            }
        }
        else
        {
            characters = new Character[3];
        }

        return characters;
    }

    public void SaveData(Character?[] characters)
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
        using FileStream file = File.Exists(filePath)
            ? File.Open(filePath, FileMode.Truncate)
            : File.Open(filePath, FileMode.Create);

        JsonSerializer.SerializeAsync(file, saveData);
    }
}