﻿using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace ZzabDenRing.Screens.Status
{
    
    internal class StatusScreen : BaseScreen
    {
        private Stack<ScreenType> _backStack = new(10);
        internal IReadOnlyCollection<ScreenType> BackStack => _backStack;

        public Character player;
        internal StatusScreen()
        {
            player = new Character("이름","직업",100, 100, 10, 01, 5, 1500, 15, new List<Item>(),new Equipment());
        }
        protected override void DrawContent()
        {

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("상태 보기");
            Console.ResetColor(); //컬러 리셋

            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("########################################");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} "); 
            Console.WriteLine($"{player.Name}: ");
            Console.WriteLine($"{player.Job}: ");         
            Console.WriteLine($"공격력 : {player.Atk}");
            Console.WriteLine($"방어력 : {player.Def}");
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
                ConsoleKey.UpArrow => Command.MoveTop,
                ConsoleKey.RightArrow => Command.MoveRight,
                ConsoleKey.DownArrow => Command.MoveBottom,
                ConsoleKey.LeftArrow => Command.MoveLeft,
                ConsoleKey.Enter => Command.Interaction,
                ConsoleKey.X => Command.Exit,
                _ => Command.Nothing
            };
            

            return command != Command.Exit;
        }
        

    }
}
