using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class Repository
{
    private readonly IDataSource _source;
    private static Repository? _instance;
    private long _time;
    public Character[] Characters { get; private set; }
    private int _selectedIndex = 0;
    public Character? Character => Characters.ElementAtOrDefault(_selectedIndex);

    public Task? CurrentTask;
    public Shopper Shopper;

    public static Repository GetInstance()
    {
        return _instance ??= new(new LocalDataSource());
    }

    private Repository(IDataSource source)
    {
        _source = source;
        LoadCharacters();
        Shopper = _source.GetSellingItems();
        Shopper.SellingItems = Shopper.SellingItems.OrderBy(it => it.Grade).ToList();
        _time = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
    }

    public void LoadCharacters()
    {
        Characters = _source.GetCharacters();
        CurrentTask = null;
    }

    public void SelectCharacter(int index)
    {
        // todo : 예외 확인 후(존재 하지 않는 idx의 경우에 캐릭터를 새로 생성하도록 처리 )후 선택

        _selectedIndex = index;
    }

    public void SaveData()
    {
        _source.SaveData(Characters);
    }

    public void CreateCharacter(Character c)
    {
        Characters[_selectedIndex] = c;
        _source.SaveData(Characters);
    }

    public void BuyItem(IItem item)
    {
        Character.Inventory.Add(item);
        Character.Gold -= item.Price;
    }

    public void SellItem(IItem sellItem)
    {
        var delete = Character.Inventory.Find(item => item.Name == sellItem.Name);
        Character.Inventory.Remove(delete);
        Character.Gold += sellItem.Price / 10 * 3;
    }

    public void DeleteCharacter(int idx)
    {
        Characters[idx] = null;
    }

    public void Enhance(int stone)
    {
        for (var i = 0; i < stone; i++)
        {
            var idx = Character.Inventory.FindIndex(item => item is MaterialItem);
            if (idx != -1)
            {
                Character.Inventory.RemoveAt(idx);
            }
        }
    }
}