using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Dungeon
{
    public class DungeonBattleScreen : BaseScreen
    {
        protected override void DrawContent()
        {
            DungeonBattleScreenShow();
        }

        protected override bool ManageInput()
        {
            var key = ReadKey();
            var command = key.Key switch
            {
                ConsoleKey.X => Command.Exit,
                _ => Command.Nothing
            };
            return command != Command.Exit;
        }

        public List<Monster> monsters = new List<Monster>();


        public DungeonBattleScreen()
        {

        }

        private void DungeonBattleScreenShow()
        {
            for (int i = 0; i < monsters.Count; i++)
            {
                WriteLine($"1. Lv. {monsters[i].Level} {monsters[i].Name}  HP {monsters[i].Hp}");
            }

            WriteLine("[내정보]");
            WriteLine($"Lv.(Player.Lv) (Player.Name) ((Player.Job))");
            WriteLine($"HP (Player.MaxHp)/(Player.Hp)");

            WriteLine("0. 다음");
        }
    }
}
