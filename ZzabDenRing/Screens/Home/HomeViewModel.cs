using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Home;

public class HomeViewModel
{
    public IAsyncEnumerable<HomeSplashState> AnimationState { get; private set; }
    private HomeState _homeState;
    public HomeState HomeState => _homeState;


    public HomeViewModel()
    {
        // todo get characters from repo
        var characters = new List<Character>();
        characters.AddRange(new[] { Game.C1, Game.C2, Game.C3 });
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
                break;
        }
    }
}

public record HomeSplashState(
    int CenterX,
    int CenterY,
    string CurrentHomeString = ""
);

public record HomeState(
    int CurX,
    IReadOnlyCollection<Character> Characters);