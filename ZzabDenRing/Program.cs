// See https://aka.ms/new-console-template for more information

using ZzabDenRing;
using ZzabDenRing.Screens.Equipment;

public class Program
{
    public static void Main(string[] args)
    {
        // var game = new Game();
        // game.Start();

        new EquipmentScreen(() => { }).DrawScreen();
    }
}