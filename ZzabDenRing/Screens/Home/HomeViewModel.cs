using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Home;

public class HomeViewModel
{
    public IAsyncEnumerable<HomeSplashState> AnimationState { get; private set; }
    private HomeState _homeState;
    private Repository _repo;
    public HomeState HomeState => _homeState;
    public Task? Loading;

    public HomeViewModel()
    {
        _repo = Container.GetRepository();
        // todo get characters from repo
        Loading = _repo.CurrentTask;
        var characters = _repo.Characters;
        AnimationState = GetHomeState();
        _homeState = new HomeState(0, characters);
    }

    private async IAsyncEnumerable<HomeSplashState> GetHomeState()
    {
        var lines = Constants.HomeAsciiArt.Split("\n");
        int centerX = lines.Max(s => s.Length) / 2;
        int centerY = lines.Length / 2;
        var homeState = new HomeSplashState(CurrentHomeString: "",
            CenterX: centerX,
            CenterY: centerY);

        for (var i = 1; i <= Constants.HomeAsciiArt.Length; i++)
        {
            //await for 2 sec
            await Task.Delay((int)Math.Ceiling(Constants.HomeAsciiArt.Length / 2_000f));
            yield return homeState with { CurrentHomeString = Constants.HomeAsciiArt[..i] };
        }
    }

    public void OnCommand(Command command)
    {
        switch (command)
        {
            case Command.MoveRight:
                if (_homeState.CurX < HomeScreen.CharacterSlots - 1)
                {
                    _homeState = _homeState with { CurX = _homeState.CurX + 1 };
                }

                break;
            case Command.MoveLeft:
                if (_homeState.CurX > 0)
                {
                    _homeState = _homeState with { CurX = _homeState.CurX - 1 };
                }

                break;
            case Command.Interaction:
                //todo 캐릭터 생성 or 캐릭터 선택하도록 메모리 변경
                _repo.SelectCharacter(_homeState.CurX);
                if (_repo.Character == null)
                {
                    _homeState = _homeState with { CreateCharacter = true };
                }

                break;
        }
    }

    public void CreateCharacter()
    {
        //todo : delete create screen 만들고 나서
        var character = new Character(
            "용사",
            "전사",
            200,
            200,
            10,
            1,
            10,
            20_000,
            10,
            new List<IItem>(),
            new());
        _repo.CreateCharacter(character);
    }
}

public record HomeSplashState(
    int CenterX,
    int CenterY,
    string CurrentHomeString = ""
);

public record HomeState(
    int CurX,
    IReadOnlyCollection<Character?> Characters,
    bool CreateCharacter = false);