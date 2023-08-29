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
        //Height = 20;
    }

    protected override void DrawContent()
    {
        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("���� ȭ��");
        Console.ResetColor();
        Console.WriteLine();

        Console.WriteLine("1. ���º���");
        Console.WriteLine("2. �κ��丮");
        Console.WriteLine("3. ���â");
        Console.WriteLine("4. ����");
        Console.WriteLine("5. ����");
        Console.WriteLine();
        Console.WriteLine("0. ����ȭ��");
        Console.WriteLine();

        Console.WriteLine("안녕하세요.");
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