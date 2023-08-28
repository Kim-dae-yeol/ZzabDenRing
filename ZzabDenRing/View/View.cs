namespace ZzabDenRing.View;

public class View : IView
{
    public int Left = 0;
    public int Top = 0;
    public int Width = 0;
    public int Height = 0;
    public bool IsVisible = true;
    
    public virtual void Draw()
    {
    }
}