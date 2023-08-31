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
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\r\n  __  __       _       \r\n |  \\/  |     (_)      \r\n | \\  / | __ _ _ _ __  \r\n | |\\/| |/ _` | | '_ \\ \r\n | |  | | (_| | | | | |\r\n |_|  |_|\\__,_|_|_| |_|\r\n                       \r\n                       \r\n");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 던전입장");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 상점");
        Console.WriteLine("5. 장비창");
        Console.WriteLine();
        Console.WriteLine("X. 시작화면");
        Console.WriteLine();

        Console.WriteLine("원하는 행동을 입력해주세요.");
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