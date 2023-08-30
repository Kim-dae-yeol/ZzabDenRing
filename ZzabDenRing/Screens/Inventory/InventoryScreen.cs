using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Model;
using ZzabDenRing.Screens.Main;
using static System.Console;

namespace ZzabDenRing.Screens.Inventory
{
    internal class InventoryScreen : BaseScreen
    {
    
        
        int SelectIndex = 0;
        Item[] arrItem;
        int itemX;

        protected override void DrawContent()
        {
            Console.Clear();
            Render();
            DataSetting();


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
        
        public InventoryScreen(int x, int y)
        {
            if (1 > x) // 인벤토리 크기가 0보다 작으면 안 되니까
            {
                x = 1;
            }

            if (1 > y)
            {
                y = 1;
            }

            itemX = x;

            arrItem = new Item[(x * y)];
        }

        public void ItemIn(Item item)
        {
            int index = 0;

            for (int i=0; i<arrItem.Length; i++)
            {
                if (arrItem[i] == null)
                {
                    index = i;
                    break;
                }
            }
            arrItem[index] = item;
        }
        public void ItemIn(Item item, int order)
        {
            if (null != arrItem[order])
            {
                return;
            }
            arrItem[order] = item;
        }

        public bool OverCheck(int selectIndex)
        {
            return false;
        }

        public void SelectMoveLeft()
        {
            int checkIndex = SelectIndex;
            checkIndex -= 1;
            // 방어코드

            if (OverCheck(checkIndex) == true)
            {
                return;
            }

            SelectIndex -= 1;
        }

        public void SelectMoveRight()
        {
            int checkIndex = SelectIndex;
            checkIndex += 1;
            // 방어코드

            if (OverCheck(checkIndex) == true)
            {
                return;
            }

            SelectIndex += 1;
        }

        public void SelectMoveUp()
        {
            int checkIndex = SelectIndex;
            checkIndex -= itemX;
            // 방어코드

            if (OverCheck(checkIndex) == true)
            {
                return;
            }

            SelectIndex -= itemX;
        }

        public void SelectMoveDown()
        {
            int checkIndex = SelectIndex;
            checkIndex += itemX;
            // 방어코드

            if (OverCheck(checkIndex) == true)
            {
                return;
            }

            SelectIndex += itemX;
        }

        public void Render()
        {
            Console.WriteLine();
            Console.WriteLine(" ★ 인벤토리 ★");
            Console.WriteLine();

            for (int i=0; i<arrItem.Length; i++)
            {
                if (i != 0 && i % itemX == 0)
                {
                    Console.WriteLine(" ");
                }
                if (SelectIndex == i)
                {
                    Console.Write(" ★");
                }
                else if (null == arrItem[i])
                {
                    Console.Write(" □");
                }
                else
                {
                    Console.Write(" ■");
                }
            }

            Console.WriteLine("\n");
            if (arrItem[SelectIndex]  != null)
            {
                Console.Write(" 현재 선택한 아이템: ");
                Console.WriteLine($"{arrItem[SelectIndex].Name} +{arrItem[SelectIndex].Enhancement} ({arrItem[SelectIndex].Type})");
                Console.WriteLine();
                Console.WriteLine(" 공격력 : " + arrItem[SelectIndex].Atk);
                Console.WriteLine(" 방어력 : " + arrItem[SelectIndex].Def);
                Console.WriteLine(" 치명타 : " + arrItem[SelectIndex].Critical);
                Console.WriteLine(" 체력 : " + arrItem[SelectIndex].Hp);
                Console.WriteLine(" 가격 : " + arrItem[SelectIndex].Price);
                Console.WriteLine(" 종류 : " + arrItem[SelectIndex].Type);
                Console.WriteLine();
                Console.WriteLine($"☆{arrItem[SelectIndex].Desc}☆");
            }
            else
            {
                Console.WriteLine("현재 선택한 아이템");
                Console.WriteLine("아이템이 비어 있습니다!");
            }
        }

        public void DataSetting()
        {
            InventoryScreen NewInven = new InventoryScreen(10, 10);

            NewInven.ItemIn(new Item(name: "군주 직검", desc: "군주가 사용하던 직검이다.", enhancement: 1, ItemType.Weapon, atk: 5, def: 0, critical: 1, hp: 0, price: 30));

            while (true)
            {
                Console.Clear();
                NewInven.Render();
                ConsoleKeyInfo checkKey = Console.ReadKey();

                switch (checkKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        NewInven.SelectMoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        NewInven.SelectMoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        NewInven.SelectMoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        NewInven.SelectMoveDown();
                        break;
                    //case ConsoleKey.D0:
                    //    MainScreen();
                    default:
                        break;

                }
            }
        }
    }
}