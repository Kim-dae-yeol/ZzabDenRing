using System.Text.Json;
using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class LocalDataSource : IDataSource
{
    private const string FileName = "ZzabDenRing.json";
    private const string ShopperFileName = "items.json";

    public Character[] GetCharacters()
    {
        Character?[] characters;
        var parent = Environment.CurrentDirectory;
        var filePath = Path.Combine(parent, FileName);

        if (File.Exists(filePath))
        {
            try
            {
                using var file = File.OpenRead(filePath);
                var appData = JsonSerializer.Deserialize<AppData>(file);
                characters = appData.Characters;
                foreach (var pair in characters.Select((c, i) => new { i, c }))
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
                    list.AddRange(useItems);
                    list.AddRange(equipItems);
                    list.AddRange(materialItems);
                    character.Inventory = list;
                }
            }
            catch (JsonException e)
            {
                Console.WriteLine("파일이 손상되었습니다.");
                Thread.Sleep(500);
                characters = new Character[3];
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
        var json = JsonSerializer.Serialize(saveData);
        File.WriteAllText(filePath, json);
    }

    public Shopper GetSellingItems()
    {
        var filePath = Path.Combine(Environment.CurrentDirectory, ShopperFileName);
        using var file = File.OpenRead(filePath);
        return JsonSerializer.Deserialize<Shopper>(file);
    }
}