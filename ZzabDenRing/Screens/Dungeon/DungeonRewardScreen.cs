using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Dungeon
{
    public class DungeonRewardScreen : BaseScreen
    {
        public Character player;
        public Reward items;
        public Reward reward;

        private Action _navToDungeonEntrance;
        private Action _navToMain;
        public DungeonRewardScreen(Action navToDungeonEntrance,
        Action navToMain)       
        {
            _navToMain = navToMain;
            _navToDungeonEntrance = navToDungeonEntrance;            
        }
        protected override void DrawContent()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\r\n  ____        _   _   _      _            _____                 _ _   \r\n |  _ \\      | | | | | |    | |          |  __ \\               | | |  \r\n | |_) | __ _| |_| |_| | ___| |  ______  | |__) |___  ___ _   _| | |_ \r\n |  _ < / _` | __| __| |/ _ \\ | |______| |  _  // _ \\/ __| | | | | __|\r\n | |_) | (_| | |_| |_| |  __/_|          | | \\ \\  __/\\__ \\ |_| | | |_ \r\n |____/ \\__,_|\\__|\\__|_|\\___(_)          |_|  \\_\\___||___/\\__,_|_|\\__|\r\n                                                                      \r\n                                                                      \r\n");
            Console.ResetColor();

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Victory");
            Console.ResetColor();

            Console.WriteLine($"남은 체력 : {player.Hp}");
            Console.WriteLine($"획득 아이템 :{items.Items} ");
            Console.WriteLine($"획득 골드 : {reward.Gold}");

            Console.WriteLine("1. 마을로");
            Console.WriteLine("2. 계속 전투");
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
                    _navToMain();
                    break;
                case ConsoleKey.D2:
                    _navToDungeonEntrance();
                    break;              
            }

            return false;
        }
    }
}
