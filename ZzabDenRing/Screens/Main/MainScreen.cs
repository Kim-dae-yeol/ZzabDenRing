using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZzabDenRing.Screens.Main;

public class MainScreen : BaseScreen
{
    private Action _navToShop;
    private Action _navToDungeonEntrance;
    private Action _navToStatus;
    private Action _navToEquipment;
    private Action _navToInventory;
    private Action _navToHome;

    private int _contentWidth = 80;
    private int _contentHeight = 24;
    private int ContentPadding => 16
    ;
    
    private int ContentLeft => Left + Width / 2 - _contentWidth / 2;
    private int ContentTop => Height / 2 - _contentHeight / 2 + Top;

    public MainScreen(
        Action navToShop,
        Action navToDungeonEntrance,
        Action navToStatus,
        Action navToHome,
        Action navToEquipment, Action navToInventory)
    {
        _navToShop = navToShop;
        _navToDungeonEntrance = navToDungeonEntrance;
        _navToStatus = navToStatus;
        _navToEquipment = navToEquipment;
        _navToInventory = navToInventory;
        _navToHome = navToHome;
    }

    protected override void DrawContent()
    {
        this.DrawBorder(ContentLeft, ContentTop, _contentWidth, _contentHeight);

        ForegroundColor = ConsoleColor.Yellow;
        SetCursorPosition(ContentLeft + ContentPadding, ContentTop + 1);
        var lines = Constants.MainAsciiArt.Split('\n');
        foreach (var line in lines)
        {
            SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
            WriteLine(line);
        }

        ResetColor();

        WriteLine();
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);

        WriteLine("1. 상태 보기");
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
        WriteLine("2. 던전입장");
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
        WriteLine("3. 인벤토리");
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
        WriteLine("4. 상점");
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
        WriteLine("5. 장비창");
        WriteLine();
        SetCursorPosition(ContentLeft+ ContentPadding, CursorTop);
        WriteLine("X. 시작화면");

        WriteLine();
        SetCursorPosition(ContentLeft + ContentPadding, CursorTop);
        WriteLine("원하는 행동을 입력해주세요.");
    }

    protected override bool ManageInput()
    {
        var key = ReadKey();
        var command = key.Key switch
        {
            _ => Command.Exit
        };

        switch (key.Key)
        {
            case ConsoleKey.D1:
                _navToStatus();
                break;
            case ConsoleKey.D2:
                _navToDungeonEntrance();
                break;
            case ConsoleKey.D3:
                _navToInventory();
                break;
            case ConsoleKey.D4:
                _navToShop();
                break;
            case ConsoleKey.D5:
                _navToEquipment();
                break;
            case ConsoleKey.X:
                _navToHome();
                break;
        }

        return false;
    }
}