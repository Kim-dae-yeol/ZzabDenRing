using static System.Console;

namespace ZzabDenRing.Screens.Home;

public class HomeScreen : BaseScreen
{
    private HomeViewModel _vm = new();

    private bool _isSplashStarted;
    private bool _isSplashFinished;

    private bool IsSplashStarted
    {
        get
        {
            lock (this)
            {
                return _isSplashStarted;
            }
        }
        set
        {
            lock (this)
            {
                _isSplashStarted = value;
            }
        }
    }

    private bool IsSplashFinished
    {
        get
        {
            lock (this)
            {
                return _isSplashFinished;
            }
        }
        set
        {
            lock (this)
            {
                _isSplashFinished = value;
            }
        }
    }

    protected override void DrawContent()
    {
        if (IsSplashStarted) return;

        IsSplashStarted = true;
        Splash();
    }

    private async void Splash()
    {
        await foreach (var state in _vm.State)
        {
            var lines = state.currentHomeString.Split("\n");
            SetCursorPositionToContentBlock();
            var idx = 0;
            foreach (var line in lines)
            {
                SetCursorPosition(Width / 2 - state.centerX, (idx++) + Height / 2 - state.centerY);
                WriteLine(line);
            }
        }

        IsSplashFinished = true;
    }

    protected override bool ManageInput()
    {
        var key = ReadKey();
        var command = key.Key switch
        {
            ConsoleKey.X => Command.Exit,
            _ => Command.Nothing
        };
        
        return !IsSplashFinished || command != Command.Exit;
    }
}