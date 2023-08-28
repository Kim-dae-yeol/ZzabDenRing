namespace ZzabDenRing;

public class Game
{
    // todo screen display
    // todo di
    // todo database or file system
    private ScreenDisplay _display = new();
    
    public void Start()
    {
        _display.Display();
    }
}