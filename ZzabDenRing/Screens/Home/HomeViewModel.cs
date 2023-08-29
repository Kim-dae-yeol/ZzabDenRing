namespace ZzabDenRing.Screens.Home;

public class HomeViewModel
{
    private IAsyncEnumerable<HomeState> _state;
    public IAsyncEnumerable<HomeState> State => _state;

    public HomeViewModel()
    {
        _state = GetHomeState();
        
    }

    private async IAsyncEnumerable<HomeState> GetHomeState()
    {
        var lines = Constants.HomeAsciiArt.Split("\n");
        int centerX = lines.Max(s => s.Length) / 2;
        int centerY = lines.Length / 2;
        var homeState = new HomeState(currentHomeString: "",
            centerX: centerX,
            centerY: centerY);

        for (var i = 1; i <= Constants.HomeAsciiArt.Length; i++)
        {
            //await for 2 sec
            await Task.Delay((int)Math.Ceiling(Constants.HomeAsciiArt.Length / 2_000f));
            yield return homeState with { currentHomeString = Constants.HomeAsciiArt[..i] };
        }
    }
}

public record HomeState(
    int centerX,
    int centerY,
    string currentHomeString = ""
);