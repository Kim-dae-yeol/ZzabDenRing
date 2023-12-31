﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;
using ZzabDenRing.Screens.Main;
using static System.Console;

namespace ZzabDenRing.Screens.Inventory
{
    internal class InventoryScreen : BaseScreen
    {
        int SelectIndex = 0;
        IItem[] arrItem;
        private Repository _repository;
        int itemX;
        int x = 10;
        int y = 10;
        Action _popBackStack;

        protected override void DrawContent()
        {
            Console.Clear();
            Render();
            DataSetting();
        }

        protected override bool ManageInput()
        {
            return false;
        }

        public InventoryScreen(Action popBackStack) // 생성자
        {
            Console.Clear();
            _popBackStack = popBackStack;
            _repository = Container.GetRepository(); // 의존성 주입 흉내 -> 수동적 의존성 주입  

            if (1 > x) // 인벤토리 크기가 0보다 작으면 안 되니까

            {
                x = 1;
            }

            if (1 > y)
            {
                y = 1;
            }

            itemX = x;


            arrItem = new IItem[(x * y)];
            var character = _repository.Character ?? throw new NullReferenceException();
            for (var i = 0; i < character.Inventory.Count && i < x * y; i++)
            {
                arrItem[i] = character.Inventory[i];
            }
        }

        public void ItemIn(IItem item)
        {
            int index = 0;

            for (int i = 0; i < arrItem.Length; i++)
            {
                if (arrItem[i] == null)
                {
                    index = i;
                    break;
                }
            }

            arrItem[index] = item;
        }

        public void ItemIn(IItem item, int order)
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

            if (SelectIndex > 0)
            {
                SelectIndex -= 1;
            }
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

            if (SelectIndex < 99)
            {
                SelectIndex += 1;
            }
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

            if (SelectIndex > 9)
            {
                SelectIndex -= itemX;
            }
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

            if (SelectIndex < 90)
            {
                SelectIndex += itemX;
            }
        }

        public void Render()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(" ★ ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("인벤토리");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" ★ ");
            Console.ResetColor();
            Console.WriteLine();

            for (int i = 0; i < arrItem.Length; i++)
            {
                if (i != 0 && i % itemX == 0)
                {
                    Console.WriteLine(" ");
                }

                if (SelectIndex == i)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" ★");
                    Console.ResetColor();
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
            if (arrItem[SelectIndex] != null)
            {
                Console.Write(" 현재 선택한 아이템: ");
                Console.WriteLine($"{arrItem[SelectIndex].Name} + ({arrItem[SelectIndex].Type})");
                Console.WriteLine();

                if (arrItem[SelectIndex].Grade == ItemGrade.Legendary)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" 등급 : " + arrItem[SelectIndex].Grade);
                    Console.ResetColor();
                }
                else if (arrItem[SelectIndex].Grade == ItemGrade.Epic)
                {
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    Console.WriteLine(" 등급 : " + arrItem[SelectIndex].Grade);
                    Console.ResetColor();
                }
                else if (arrItem[SelectIndex].Grade == ItemGrade.Rare)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" 등급 : " + arrItem[SelectIndex].Grade);
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine(" 등급 : " + arrItem[SelectIndex].Grade);
                }
                Console.WriteLine();
                Console.WriteLine(" 가격 : " + arrItem[SelectIndex].Price);
                Console.WriteLine();
                Console.WriteLine($"☆{arrItem[SelectIndex].Desc}☆");
            }
            else
            {
                Console.Write("현재 선택한 아이템: ");
                Console.WriteLine("아이템이 비어 있습니다!");
            }
        }

        public void DataSetting()
        {
            bool flag = true;
            while (flag)
            {
                Console.Clear();
                Render();
                ConsoleKeyInfo checkKey = Console.ReadKey();

                switch (checkKey.Key)
                {
                    case ConsoleKey.LeftArrow:
                        SelectMoveLeft();
                        break;
                    case ConsoleKey.RightArrow:
                        SelectMoveRight();
                        break;
                    case ConsoleKey.UpArrow:
                        SelectMoveUp();
                        break;
                    case ConsoleKey.DownArrow:
                        SelectMoveDown();
                        break;
                    default:
                        flag = false;
                        break;
                }
            }

            _popBackStack();
        }
    }
}