namespace ZzabDenRing.Input;

public class ReadKeyManager
{
    public Command ReadCommand()
    {
        var key = Console.ReadKey();
        return key switch
        {
            
            _ => Command.Nothing
        };
    }
}