using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class Shopper
{
    [JsonInclude] public List<EquipItem> SellingItems;

    public Shopper(List<EquipItem> sellingItems)
    {
        SellingItems = sellingItems;
    }
}