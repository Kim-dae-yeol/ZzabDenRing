using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZzabDenRing.Screens.Main;

public class MainScreen : BaseScreen
{
    private Action _popBackStack;
    private Action _navToShop;
    private Action _navToDungeonEntrance;
    private Action _navToStatus;
    private Action _navToEquipment;

    public MainScreen(Action popBackStack,
        Action navToShop,
        Action navToDungeonEntrance,
        Action navToStatus,
        Action navToEquipment)
    {
        _popBackStack = popBackStack;
        _navToShop = navToShop;
        _navToDungeonEntrance = navToDungeonEntrance;
        _navToStatus = navToStatus;
        _navToEquipment = navToEquipment;
    }

    protected override void DrawContent()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("메인 화면");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("1. 상태 보기");
        Console.WriteLine("2. 던전입장");
        Console.WriteLine("3. 인벤토리");
        Console.WriteLine("4. 상점");
        Console.WriteLine("5. 장비창");
        Console.WriteLine();
        Console.WriteLine("6. 시작화면");
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
                //navTo();
                break;
            case ConsoleKey.D4:
                //();
                break;
            case ConsoleKey.D5:
                _navToEquipment();
                break;
            case ConsoleKey.D6:
                _popBackStack();
                break;
        }

        return false;
    }

    private int CheckValidInput(int min, int max) //�Էµ� ���ڰ� valid������ üũ
    {
        while (true)
        {
            string input = Console.ReadLine();

            bool parseSuccess = int.TryParse(input, out var ret);
            if (parseSuccess)
            {
                if (ret >= min && ret <= max)
                    return ret;
            }

            Console.WriteLine("failed");
        }
    }
}