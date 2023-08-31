using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class Shopper
{
    [JsonInclude] public List<EquipItem> SellingItems;
    [JsonInclude] public List<MaterialItem> SellingMaterial;
    [JsonInclude] public List<UseItem> SellingUse;

    public Shopper(List<EquipItem> sellingItems, List<MaterialItem> sellingMaterial, List<UseItem> sellingUse)
    {
        SellingItems = sellingItems;
        SellingMaterial = sellingMaterial;
        SellingUse = sellingUse;
    }
}