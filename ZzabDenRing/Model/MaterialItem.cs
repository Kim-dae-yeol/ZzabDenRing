using System.Text.Json.Serialization;

namespace ZzabDenRing.Model;

public class MaterialItem : IItem
{
    public MaterialItem(string name, string desc, int price, ItemType type, ItemGrade grade)
    {
        Name = name;
        Desc = desc;
        Price = price;
        Type = type;
        Grade = grade;
    }

    [JsonInclude] public string Name { get; }
    [JsonInclude] public string Desc { get; }
    [JsonInclude] public int Price { get; }
    [JsonInclude] public ItemType Type { get; }
    [JsonInclude] public ItemGrade Grade { get; }
}