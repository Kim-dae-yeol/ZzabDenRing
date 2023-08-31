using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using ZzabDenRing.Data;

namespace ZzabDenRing.Screens.Status
{
    internal class StatusScreen : BaseScreen
    {
        private Action _navToMain;
        public Character player;

        //public StatusScreen(Action navToMain)
        //{
        //    _navToMain = navToMain;
        //    player = new Character("이름", "직업", 200, 100, 10, 1, 5, 1500, 15, new List<IItem>(),
        //        new Model.Equipment());
        //}
        public StatusScreen(Action navToMain, Repository repository)
        {
            _navToMain = navToMain;
            player = repository.Character;
            
        }       

        protected override void DrawContent()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(
                "\r\n _______ __          __               \r\n|     __|  |_.---.-.|  |_.--.--.-----.\r\n|__     |   _|  _  ||   _|  |  |__ --|\r\n|_______|____|___._||____|_____|_____|\r\n  \r\n");

            Console.ResetColor(); //컬러 리셋

            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("########################################");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} ");
            Console.WriteLine($"이름 : {player.Name} ");
            Console.WriteLine($"직업 : {player.Job} ");
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
            Console.WriteLine($"크리티컬 : {player.Critical}");
            Console.WriteLine($"체력 : {player.Hp}");

            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"Gold : {player.Gold}");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("########################################");

            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">>>");
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
                case ConsoleKey.D0:
                    _navToMain();
                    break;
            }

            return false;
        }
    }
}