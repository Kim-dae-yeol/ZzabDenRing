﻿using System;
using static System.Console;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using ZzabDenRing.Data;
using ZzabDenRing.Screens.Equipment;

namespace ZzabDenRing.Screens.Status
{
    internal class StatusScreen : BaseScreen
    {
        private Action _navToMain;
        public Character player;
        public EquipItem equipItem;
        

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

            Console.ResetColor(); 

            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();
            Console.WriteLine("########################################");
            Console.WriteLine();
            Console.WriteLine($"Lv.{player.Level} ");
            Console.WriteLine($"이름 : {player.Name} ");
            Console.WriteLine($"직업 : {player.Job} ");
            //Console.WriteLine($"공격력 : {player.Atk + player.Equipment.AddedAtk()}");
            //Console.WriteLine($"방어력 : {player.Def + player.Equipment.AddedDef()}");
            Console.WriteLine($"공격력 : {player.Atk} + ({player.Equipment.AddedAtk()})");
            Console.WriteLine($"방어력 : {player.Def} + ({player.Equipment.AddedDef()})");

            Console.WriteLine($"크리티컬 : {player.Critical + player.Equipment.AddedCritical()}");
            Console.WriteLine($"체력 : {player.Hp + player.Equipment.AddedHp()}");

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