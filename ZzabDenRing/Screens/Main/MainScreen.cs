
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
        Console.WriteLine("1. 상태보기");
        Console.WriteLine("2. 인벤토리");
        Console.WriteLine("3. 장비창");
        Console.WriteLine("4. 상점");
        Console.WriteLine("5. 던전");
        Console.WriteLine();
        Console.WriteLine("0. 시작화면");

        Console.WriteLine("원하시는 행동을 입력해주세요.");

        

        
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
            return false; // 종료 명령이 들어왔을 경우, 화면을 빠져나가기 위해 false 반환
        }

        int selectedAction = CheckValidInput(0, 5); 

        switch (selectedAction)
        {
            case 0:
                _backStack.Push(ScreenType.Home); // 시작 화면으로 이동
                break;
            case 1:
                // TODO: 상태보기 화면으로 이동
                break;
            case 2:
                // TODO: 인벤토리 화면으로 이동
                break;
            case 3:
                // TODO: 장비창 화면으로 이동
                break;
            case 4:
                // TODO: 상점 화면으로 이동
                break;
            case 5:
                // TODO: 던전 화면으로 이동
                break;
        }

        return true; 
    }

    static int CheckValidInput(int min, int max) //입력된 숫자가 valid한지를 체크
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

            Console.WriteLine("잘못된 입력입니다.");
        }
    }

}
