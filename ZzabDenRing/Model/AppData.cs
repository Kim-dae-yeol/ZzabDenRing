using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class AppData
{
    [JsonInclude] // include this field
    public Character[] Characters;

    [JsonInclude]
    public List<EquipItem[]> InventoryForEquip;
    [JsonInclude]
    public List<MaterialItem[]> InventoryForMaterial;
    [JsonInclude]
    public List<UseItem[]> InventoryForUse;

    public AppData(Character[] characters, List<EquipItem[]> inventoryForEquip, List<MaterialItem[]> inventoryForMaterial, List<UseItem[]> inventoryForUse)
    {
        Characters = characters;
        InventoryForEquip = inventoryForEquip;
        InventoryForMaterial = inventoryForMaterial;
        InventoryForUse = inventoryForUse;
    }
}