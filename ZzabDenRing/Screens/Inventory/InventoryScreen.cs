using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using static System.Console;

namespace ZzabDenRing.Screens.Inventory
{
    internal class InventoryScreen : BaseScreen
    {
        private Character _character;
        private List<IItem> inventory;
        private int _selectedIndex = -1;

        protected override void DrawContent()
        {
            Console.Clear();
            DataSetting();
            InventoryDisplay();
        }

        protected override bool ManageInput()
        {
            var key = Console.ReadKey();
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


        private void DataSetting()
        {
            // todo 저장소에서 받아야됨 
            _character = new Character(
                "name",
                "job",
                100,
                100,
                10,
                1,
                100,
                10_000,
                10,
                Game.Items.Take(10).ToList(),
                Game.Equipment);
            inventory = _character.Inventory;
        }

        private void InventoryDisplay()
        {
            Console.Clear();

            Console.WriteLine(
                " ########   ##    #   #       #   #######   ##    #   ########     ####     #####    #     #  ");
            Console.WriteLine(
                "    ##      # #   #    #     #    ##        # #   #      ##       #    #    #    #    #   #   ");
            Console.WriteLine(
                "    ##      #  #  #     #   #     #######   #  #  #      ##      #      #   #####      ###    ");
            Console.WriteLine(
                "    ##      #   # #      # #      ##        #   # #      ##       #    #    #   #       #     ");
            Console.WriteLine(
                " ########   #    ##       #       #######   #    ##      ##        ####     #    #      #     ");
            Console.WriteLine("\n");
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i] == null)
                {
                    break;
                }

                WriteLine($"                                    {i + 1}. {inventory[i].Name}\n");

                if (_selectedIndex == i)
                {
                }
            }

            WriteLine($"                                  소유한 골드: {_character.Gold}");
            WriteLine();
            WriteLine("                                  0. 나가기");
            WriteLine();
            Write("                                 >> ");
            string? input = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(input, out int x))
            {
                if (x == 0)
                {
                    WriteLine("메인화면");
                }
                else if (x > 0 && x <= inventory.Count)
                {
                    // [0,length)
                    // null 값 
                    var index = x - 1;
                    if (_selectedIndex == index)
                    {
                        _selectedIndex = -1;
                    }
                    else
                    {
                        _selectedIndex = index;
                    }

                    InventoryDisplay();
                }
            }
        }
    }
}