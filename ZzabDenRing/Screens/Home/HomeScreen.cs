using static System.Console;

namespace ZzabDenRing.Screens.Home;

public class HomeScreen : BaseScreen
{
    private HomeViewModel _vm = new();

    private bool _isSplashStarted;
    private bool _isSplashFinished;

    private Action _navToMain;
    private Action _popBackStack;

    public HomeScreen(Action navToMain, Action popBackStack)
    {
        _navToMain = navToMain;
        _popBackStack = popBackStack;
        Height = 25;
        ClearScreenWhenRedraw = false;
    }

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
        if (IsSplashFinished)
        {
            var message = "1. 캐릭터 선택";
            var message2 = "2. 종료하기";
            SetCursorPosition(Width / 2 - message.LengthToDisplay() / 2, Height / 2 - 1);
            WriteLine(message);
            SetCursorPosition(Width / 2 - message.LengthToDisplay() / 2, CursorTop);
            WriteLine(message2);
            return;
        }

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
            if (IsSplashFinished) break;

            foreach (var line in lines)
            {
                if (IsSplashFinished) break;
                SetCursorPosition(Width / 2 - state.centerX, (idx++) + Height / 2 - state.centerY);
                WriteLine(line);
            }
        }

        SplashFinish();
    }

    private void SplashFinish()
    {
        Width = 100;
        Height = 10;
        ClearScreenWhenRedraw = true;
        IsSplashFinished = true;
    }

    protected override bool ManageInput()
    {
        var key = ReadKey();
        if (key.Key == ConsoleKey.D1)
        {
            _navToMain();
        }
        else if (key.Key == ConsoleKey.D2)
        {
            _popBackStack();
        }

        var command = key.Key switch
        {
            ConsoleKey.Enter => Command.Interaction,
            ConsoleKey.D1 => Command.Exit,
            ConsoleKey.D2 => Command.Exit,
            _ => Command.Nothing
        };

        if (command == Command.Interaction)
        {
            IsSplashFinished = true;
            SplashFinish();
        }

        return !IsSplashFinished || command != Command.Exit;
    }

    protected override void DrawCommands()
    {
    }
}