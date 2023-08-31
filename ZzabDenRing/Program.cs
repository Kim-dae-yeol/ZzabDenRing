// See https://aka.ms/new-console-template for more information

using ZzabDenRing;
using ZzabDenRing.Di;
using ZzabDenRing.Screens.CreateCharacter;
using ZzabDenRing.Screens.Dungeon;
using ZzabDenRing.Screens.Equipment;
using ZzabDenRing.Screens.Shop;
using ZzabDenRing.Screens.Status;

public class Program
{
    public static void Main(string[] args)
    {
        Container.GetRepository().SaveData();

        var game = new Game();
        game.Start();
        // new CreateCharacterScreen(() => { }, () => { }).DrawScreen();
    }
}