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

        static Item[] inventory;  
        

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


        static void DataSetting()
        {
            inventory = new Item[10];
            inventory[0] = new Item(name: "무쇠갑옷", desc: "무쇠로 만든 갑옷입니다.", enhancement: 1, type: ItemType.Weapon, atk: 0, def: 5, critical: 0, hp: 30, price: 100);
            inventory[1] = new Item(name: "무쇠검", desc: "무쇠로 만든 검입니다.", enhancement: 1, type: ItemType.Weapon, atk: 5, def: 0, critical: 1, hp: 0, price: 50);
            inventory[2] = new Item(name: "무쇠방패", desc: "무쇠로 만든 방패입니다.", enhancement: 1, type: ItemType.Weapon, atk: 0, def: 7, critical: 0, hp: 0, price: 50);
        }
        static void InventoryDisplay()
        {
            Console.Clear();

            Console.WriteLine(" ########   ##    #   #       #   #######   ##    #   ########     ####     #####    #     #  ");
            Console.WriteLine("    ##      # #   #    #     #    ##        # #   #      ##       #    #    #    #    #   #   ");
            Console.WriteLine("    ##      #  #  #     #   #     #######   #  #  #      ##      #      #   #####      ###    ");
            Console.WriteLine("    ##      #   # #      # #      ##        #   # #      ##       #    #    #   #       #     ");
            Console.WriteLine(" ########   #    ##       #       #######   #    ##      ##        ####     #    #      #     ");
            Console.WriteLine("\n");
            for (int i = 0; i < inventory.Length; i++)
            {
                if (inventory[i] == null)
                {
                    break;
                }
                Console.WriteLine($"                                    {i + 1}. {inventory[i].Name}\n");

                if (inventory[i].isChoose)
                {
                    Console.WriteLine($"( 강화: +{inventory[i].Enhancement}, 종류: {inventory[i].Type}, 공격력: {inventory[i].Atk}, 방어력: {inventory[i].Def}, 치명타: {inventory[i].Critical}, 체력: {inventory[i].Hp}, 골드: {inventory[i].Price})\n");
                }
            }

            Console.WriteLine("                                  소유한 골드: {player.Gold}");
            Console.WriteLine();
            Console.WriteLine("                                  0. 나가기");
            Console.WriteLine();
            Console.Write("                                 >> ");
            string input = Console.ReadLine();
            Console.WriteLine();
            if (int.TryParse(input, out int x))
            {
                if (x == 0)
                {
                    Console.WriteLine("메인화면");
                }
                else if (x > 0 && x <= inventory.Length)
                {
                    Item item = inventory[x - 1];
                    if (item.isChoose)
                    {
                        UnChooseItem(item);
                    }
                    else
                    {
                        ChooseItem(item);
                    }
                    InventoryDisplay();
                }

            }
        }
        static void ChooseItem(Item item)
        {
            item.isChoose = true;
        }

        static void UnChooseItem(Item item)
        {
            item.isChoose = false;
        }
    }
}
