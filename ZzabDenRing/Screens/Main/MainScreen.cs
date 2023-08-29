
using static System.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZzabDenRing.Screens.Main;

public class MainScreen : BaseScreen
{
    private Stack<ScreenType> _backStack = new(10);

    internal IReadOnlyCollection<ScreenType> BackStack => _backStack;

    protected override void DrawContent()
    {
        Console.WriteLine("1. ���º���");
        Console.WriteLine("2. �κ��丮");
        Console.WriteLine("3. ���â");
        Console.WriteLine("4. ����");
        Console.WriteLine("5. ����");
        Console.WriteLine();
        Console.WriteLine("0. ����ȭ��");

        Console.WriteLine("���Ͻô� �ൿ�� �Է����ּ���.");

        

        
    }

    protected override bool ManageInput()
    {
        var key = ReadKey();
        var command = key.Key switch
        {
            ConsoleKey.X => Command.Exit,
            _ => Command.Nothing
        };

        if (command == Command.Exit)
        {
            return false; // ���� ����� ������ ���, ȭ���� ���������� ���� false ��ȯ
        }

        int selectedAction = CheckValidInput(0, 5); 

        switch (selectedAction)
        {
            case 0:
                _backStack.Push(ScreenType.Home); // ���� ȭ������ �̵�
                break;
            case 1:
                // TODO: ���º��� ȭ������ �̵�
                break;
            case 2:
                // TODO: �κ��丮 ȭ������ �̵�
                break;
            case 3:
                // TODO: ���â ȭ������ �̵�
                break;
            case 4:
                // TODO: ���� ȭ������ �̵�
                break;
            case 5:
                // TODO: ���� ȭ������ �̵�
                break;
        }

        return true; 
    }

    static int CheckValidInput(int min, int max) //�Էµ� ���ڰ� valid������ üũ
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

            Console.WriteLine("�߸��� �Է��Դϴ�.");
        }
    }

}
