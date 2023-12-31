﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Di;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Dungeon
{
    public class DungeonRewardScreen : BaseScreen
    {
        public const string ArgReward = "arg_reward";
        public Character player;
        public Reward _reward;

        private Action _navToDungeonEntrance;
        private Action _navToMain;

        public DungeonRewardScreen(Action navToDungeonEntrance,
            Action navToMain,
            Reward reward
        )
        {
            _navToMain = navToMain;
            _navToDungeonEntrance = navToDungeonEntrance;
            player = Container.GetRepository().Character;
            _reward = reward;
        }

        protected override void DrawContent()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(
                "\r\n  ____        _   _   _      _            _____                 _ _   \r\n |  _ \\      | | | | | |    | |          |  __ \\               | | |  \r\n | |_) | __ _| |_| |_| | ___| |  ______  | |__) |___  ___ _   _| | |_ \r\n |  _ < / _` | __| __| |/ _ \\ | |______| |  _  // _ \\/ __| | | | | __|\r\n | |_) | (_| | |_| |_| |  __/_|          | | \\ \\  __/\\__ \\ |_| | | |_ \r\n |____/ \\__,_|\\__|\\__|_|\\___(_)          |_|  \\_\\___||___/\\__,_|_|\\__|\r\n                                                                      \r\n                                                                      \r\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Victory!");
            Console.ResetColor();
            Console.WriteLine();

            Console.WriteLine($"남은 체력 : {player.Hp}");
            Console.Write($"획득 아이템 : ");

            foreach (var pair in _reward.Items.Select((item, i) => new { item, i }))
            {
                var item = pair.item;
                var i = pair.i;
                if (i == _reward.Items.Length - 1)
                {
                    Write(item.Name);
                }
                else
                {
                    Write(item.Name + ", ");
                }
            }

            WriteLine();
            Console.WriteLine($"획득 골드 : {_reward.Gold} G");
            Console.WriteLine();

            Console.WriteLine("1. 계속 전투");
            Console.WriteLine("2. 마을로");
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
                    _navToDungeonEntrance();
                    break;
                case ConsoleKey.D2:
                    _navToMain();
                    break;
            }

            return false;
        }
    }
}