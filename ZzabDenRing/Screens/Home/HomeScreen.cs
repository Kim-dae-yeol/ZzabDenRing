using System.Drawing;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Home;

public class HomeScreen : BaseScreen
{
    private HomeViewModel _vm = new();

    private bool _isSplashStarted;
    private bool _isSplashFinished;
    private bool _isClearSplash;

    private const int CharacterSlotWidth = 30 + 2;
    private const int CharacterSlotHeight = 10 + 2;
    public const int CharacterSlots = 3;
    private const int ButtonHeight = 3;
    private const int ButtonWidth = 16;

    private readonly Action _navToMain;
    private readonly Action _exitGame;
    private Action _navToCreateCharacter;

    public HomeScreen(Action navToMain, Action exitGame, Action navToCreateCharacter)
    {
        _navToMain = navToMain;
        _exitGame = exitGame;
        _navToCreateCharacter = navToCreateCharacter;
        Height = 30;
        ClearScreenWhenRedraw = false;
        CommandsWidth = 36;
        CommandsHeight = 3 + 2;
        CommandLeft = Left + Width / 2 - CommandsWidth / 2;
        CommandTop = Height - CommandsHeight - 5;
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

    protected override async void DrawContent()
    {
        if (_vm.Loading != null) await _vm.Loading;

        if (IsSplashFinished)
        {
            if (_vm.HomeState.CreateCharacter)
            {
            }
            else
            {
                DrawCharacters();
            }

            _isClearSplash = true;
            return;
        }

        if (IsSplashStarted) return;
        IsSplashStarted = true;
        Splash();
    }

    private async void Splash()
    {
        await foreach (var state in _vm.AnimationState)
        {
            var lines = state.CurrentHomeString.Split("\n");
            SetCursorPositionToContentBlock();
            var idx = 0;
            if (IsSplashFinished) break;

            foreach (var line in lines)
            {
                if (IsSplashFinished) break;
                SetCursorPosition(Width / 2 - state.CenterX, (idx++) + Height / 2 - state.CenterY);
                WriteLine(line);
            }
        }

        SplashFinish();
    }

    private void SplashFinish()
    {
        ClearScreenWhenRedraw = true;
        IsSplashFinished = true;
        ShownCommands = true;
        Thread.Sleep(50);
    }

    protected override bool ManageInput()
    {
        SetCursorPositionToCommands();
        var key = ReadKey();


        var command = key.Key switch
        {
            ConsoleKey.Enter => Command.Interaction,
            ConsoleKey.X => Command.Exit,
            ConsoleKey.D => Command.Delete,
            ConsoleKey.LeftArrow => Command.MoveLeft,
            ConsoleKey.RightArrow => Command.MoveRight,
            _ => Command.Nothing
        };


        if (IsSplashFinished && _isClearSplash)
        {
            _vm.OnCommand(command);
            switch (command)
            {
                case Command.Exit:
                    _exitGame();
                    break;
                case Command.Interaction:
                    if (_vm.HomeState.CreateCharacter)
                    {
                        _navToCreateCharacter();
                    }
                    else
                    {
                        _navToMain();
                    }

                    break;
            }
        }
        else
        {
            SplashFinish();
            return true;
        }


        return !IsSplashFinished || (command != Command.Exit && command != Command.Interaction);
    }

    protected override void DrawCommands()
    {
        SetCursorPosition(CommandLeft, CommandTop + 1);
        var lines = new string[] { "시작하기 [ Enter ]", "삭제하기 [ D ]", "종료하기 [ X ]" };
        foreach (var line in lines)
        {
            var centerAligned = CommandLeft + CommandsWidth / 2 - line.LengthToDisplay() / 2;
            SetCursorPosition(centerAligned, CursorTop);
            WriteLine(line);
        }
    }

    private void DrawCharacters()
    {
        int totalWidth = CharacterSlotWidth * CharacterSlots + 4;
        int totalHeight = CharacterSlotHeight + 2;

        int startLeft = Width / 2 - totalWidth / 2;
        int startTop = Height * 1 / 4 - totalHeight * 1 / 4;
        SetCursorPosition(startLeft, startTop);
        for (var i = 0; i < totalHeight; i++)
        {
            for (var j = 0; j < totalWidth; j++)
            {
                if (i == 0 || i == totalHeight - 1)
                {
                    if (j == 0 || j == totalWidth - 1)
                    {
                        Write("+");
                    }
                    else
                    {
                        Write("-");
                    }
                }
                else if (j == 0 || j == totalWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(startLeft, CursorTop);
        }

        for (var i = 0; i < CharacterSlots; i++)
        {
            var character = _vm.HomeState.Characters.ElementAtOrDefault(i);
            var slotLeft = startLeft + 2 + CharacterSlotWidth * i;
            var slotTop = startTop + 1;
            var isCursorOn = _vm.HomeState.CurX == i;
            DrawCharacter(character, slotLeft, slotTop, isCursorOn);
        }
    }

    private void DrawCharacter(Character? c, int startLeft, int startTop, bool isCursorOn)
    {
        SetCursorPosition(startLeft, startTop);
        for (var i = 0; i < CharacterSlotHeight; i++)
        {
            for (var j = 0; j < CharacterSlotWidth; j++)
            {
                if (i == 0 || i == CharacterSlotHeight - 1)
                {
                    if (j == 0 || j == CharacterSlotWidth - 1)
                    {
                        Write("+");
                    }
                    else
                    {
                        Write("-");
                    }
                }
                else if (j == 0 || j == CharacterSlotWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(startLeft, CursorTop);
        }

        if (c == null)
        {
            var msg = "[ + ]";
            SetCursorPosition(startLeft + CharacterSlotWidth / 2 - msg.Length / 2,
                startTop + CharacterSlotHeight / 2);
            Write(msg);
        }
        else
        {
            var name = c.Name;
            var level = c.Level;
            var job = c.Job;
            var hp = c.MaxHp;
            var atk = c.Atk;
            var def = c.Def;
            var cri = c.Critical;
            var lines = new[]
            {
                level.ToString("N0"),
                name,
                job,
                hp.ToString("N0"), atk.ToString("N0"),
                def.ToString("N0"), cri.ToString("N0")
            };

            for (var i = 0; i < lines.Length; i++)
            {
                SetCursorPosition(startLeft + 1, startTop + 1 + i);
                switch (i)
                {
                    case 0:
                        ForegroundColor = ConsoleColor.Yellow;
                        var levelText = $"Lv. {lines[i]}";
                        SetCursorPosition(
                            left: startLeft + CharacterSlotWidth / 2 - levelText.Length / 2,
                            top: CursorTop);
                        
                        WriteLine(levelText);
                        ResetColor();
                        break;
                    case 1:
                        BackgroundColor = ConsoleColor.DarkGray;
                        WriteLine($"{lines[i],-(CharacterSlotWidth - 2)}");
                        ResetColor();
                        break;
                    case 2:
                        WriteLine($"직업 : {lines[i]}");
                        break;
                    case 3:
                        Write($"[{$" Hp  {lines[i]}",-(CharacterSlotWidth - 6) / 2}]");
                        Write($"[{$" Atk  {lines[i+1]}",-(CharacterSlotWidth - 6) / 2}]");
                        break;
                    case 5:
                        SetCursorPosition(CursorLeft, CursorTop - 1);
                        Write($"[{$" Def  {lines[i]}",-(CharacterSlotWidth - 6) / 2}]");
                        Write($"[{$" Cri  {lines[i+1]}",-(CharacterSlotWidth - 6) / 2}]");
                        break;
                }
            }
        }

        if (isCursorOn)
        {
            var buttonLeft = startLeft + CharacterSlotWidth / 2 - ButtonWidth / 2;
            var buttonTop = startTop + CharacterSlotHeight - 2 - ButtonHeight;
            DrawSelectButton(buttonLeft, buttonTop, c != null);
        }
    }

    private void DrawSelectButton(int left, int top, bool isExists)
    {
        BackgroundColor = ConsoleColor.Magenta;
        SetCursorPosition(left: left, top: top);

        for (var i = 0; i < ButtonHeight; i++)
        {
            SetCursorPosition(left, CursorTop);
            for (var j = 0; j < ButtonWidth; j++)
            {
                Write(" ");
            }

            WriteLine();
        }

        var msg = isExists ? "시작하기" : "생성하기";
        SetCursorPosition(
            left: left + ButtonWidth / 2 - msg.LengthToDisplay() / 2,
            top: top + ButtonHeight / 2);
        Write(msg);
        ResetColor();
    }
}