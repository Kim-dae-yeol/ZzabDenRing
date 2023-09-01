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
    public class DungeonEntranceScreen : BaseScreen
    {
        private Action<List<Monster>> _navToBattle;
        private Action _popBackStack;

        protected override void DrawContent()
        {
            DungeonEntranceScreenShow();
        }

        protected override bool ManageInput()
        {
            var key = ReadKey();
            var command = key.Key switch
            {
                ConsoleKey.D1 => Command.Interaction,
                ConsoleKey.D2 => Command.Nothing,
                ConsoleKey.D3 => Command.Exit,
                _ => Command.Wrong
            };

            switch (command)
            {
                case Command.Interaction:
                    _navToBattle(monsters);
                    break;
                case Command.Exit:
                    _popBackStack();
                    break;
                case Command.Nothing:
                    CreateMonsters();
                    break;
            }

            return command != Command.Exit && command != Command.Interaction;
        }

        public List<Monster> monsters = new List<Monster>();

        public DungeonEntranceScreen(Action<List<Monster>> navToBattle, Action popBackStack)
        {
            _navToBattle = navToBattle;
            _popBackStack = popBackStack;
            CreateMonsters();
        }

        public DungeonEntranceScreen()
        {
        }

        private void CreateMonsters()
        {
            monsters.Clear();

            for (int i = 0; i < Random.Shared.Next(1, 4); i++)
            {
                int a = Random.Shared.Next(1, 4);
                Monster monster = a switch
                {
                    1 => new Monster(8, 3, 5, 15, 15, 2, "미니언"),
                    2 => new Monster(10, 2, 9, 10, 10, 3, "공허충"),
                    3 => new Monster(15, 5, 8, 25, 25, 5, "대포미니언"),
                    _ => new Monster(1, 1, 1, 1, 1, 1, "예외")
                };
                monsters.Add(monster);
            }
        }

        private void DungeonEntranceScreenShow()
        {
            Clear();
            ForegroundColor = ConsoleColor.Red;
            WriteLine(
                "\r\n  ____        _   _   _      _\r\n |  _ \\      | | | | | |    | |\r\n | |_) | __ _| |_| |_| | ___| |\r\n |  _ < / _` | __| __| |/ _ \\ |\r\n | |_) | (_| | |_| |_| |  __/_|\r\n |____/ \\__,_|\\__|\\__|_|\\___(_)\r\n                                                                      \r\n                                                                      \r\n");
            ResetColor();

            for (int i = 0; i < monsters.Count; i++)
            {
                WriteLine($" ▶ Lv.{monsters[i].Level}");
                WriteLine($" {monsters[i].Name}");
                WriteLine($" HP {monsters[i].MaxHp}");
                WriteLine();
            }

            for (int i = 2; i >= monsters.Count; i--)
            {
                WriteLine();
                WriteLine();
                WriteLine();
                WriteLine();
            }
            WriteLine();

            WriteLine(" 1. 전투하기");
            WriteLine(" 2. 다른 곳 둘러보기");
            WriteLine(" 3. 도망하기");
        }
    }
}