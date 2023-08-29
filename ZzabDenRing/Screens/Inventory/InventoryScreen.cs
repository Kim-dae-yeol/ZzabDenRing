using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace ZzabDenRing.Screens.Inventory
{
    internal class InventoryScreen : BaseScreen
    {
        protected override void DrawContent()
        {
            Console.Clear();
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

        

        private void InventoryDisplay()
        {
            Console.WriteLine("######################################################################"); //  ########## * 7 
            Console.WriteLine("##                                                                  ##");
            Console.WriteLine("##    >>인벤토리<<                                                  ##");
            Console.WriteLine("##                                                                  ##");
            Console.WriteLine("##    ################################################              ##");
            Console.WriteLine("##    #                                              #              ##");
            Console.WriteLine("##    #  1. 군주의 투구                              #              ##");
            Console.WriteLine("##    #  2. 군주의 목걸이                            #              ##");
            Console.WriteLine("##    #  3. 군주의 갑옷                              #              ##");
            Console.WriteLine("##    #  4. 군주의 직검                              #              ##");
            Console.WriteLine("##    #  5. 군주의 방패                              #              ##");
            Console.WriteLine("##    #  6. 군주의 각반                              #              ##");
            Console.WriteLine("##    #  7. 군주의 반지                              #              ##");
            Console.WriteLine("##    #  8. 군주의 신발                              #              ##");
            Console.WriteLine("##    #                                              #              ##");
            Console.WriteLine("##    #  1. 군주의 투구(체력: 20 방어력: 5)          #              ##");
            Console.WriteLine("##    #                                              #              ##");
            Console.WriteLine("##    ################################################              ##");
            Console.WriteLine("##                                                                  ##");
            Console.WriteLine("##    소유한 Gold:                                                  ##");
            Console.WriteLine("##                                                                  ##");
            Console.WriteLine("##    >>                                                            ##");
            Console.WriteLine("##                                                                  ##");
            Console.WriteLine("######################################################################"); //  ########## * 7




        }
    }
}
