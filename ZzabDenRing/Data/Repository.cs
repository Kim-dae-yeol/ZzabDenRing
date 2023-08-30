using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class Repository
{
    private readonly IDataSource _source;
    private static Repository? _instance;
    public Character[] Characters { get; private set; }
    private int _selectedIndex = 0;
    public Character Character => Characters[_selectedIndex];

    public static Repository GetInstance()
    {
        return _instance ??= new(new LocalDataSource());
    }

    private Repository(IDataSource source)
    {
        _source = source;
        Characters = _source.GetCharacters();
    }

    public void SelectCharacter(int index)
    {
        // todo : 예외 확인 후(존재 하지 않는 idx의 경우에 캐릭터를 새로 생성하도록 처리 )후 선택 
        _selectedIndex = index;
    }
    
}