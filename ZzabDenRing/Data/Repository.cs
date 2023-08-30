using ZzabDenRing.Model;

namespace ZzabDenRing.Data;

public class Repository
{
    private readonly IDataSource _source;
    private static Repository? _instance;
    public Character[] Characters { get; private set; }
    private int _selectedIndex = 0;
    public Character? Character => Characters.ElementAtOrDefault(_selectedIndex);

    private Task? _currentTask = null;
    public bool IsTaskComplete => _currentTask?.IsCompleted ?? true;

    public static Repository GetInstance()
    {
        return _instance ??= new(new LocalDataSource());
    }

    private Repository(IDataSource source)
    {
        _source = source;
        LoadCharacters();
    }

    public async void LoadCharacters()
    {
        var task = _source.GetCharacters();
        _currentTask = task;
        Characters = await task;
    }

    public void SelectCharacter(int index)
    {
        // todo : 예외 확인 후(존재 하지 않는 idx의 경우에 캐릭터를 새로 생성하도록 처리 )후 선택
        _selectedIndex = index;
    }

    public void SaveData()
    {
        _currentTask = _source.SaveData(Characters);
    }
}