using ZzabDenRing.View;
using static System.Console;

namespace ZzabDenRing.Screens;

public abstract class BaseScreen : IScreen
{
    protected bool ClearScreenWhenRedraw = true;
    public int Width = 150;
    public int Height = 30;
    public int Left = 0;
    public int Top = 0;

    protected int ContentLeft => Left + 1;
    protected int ContentTop => Top + 1;

    protected List<IView> Views = new();


    public void DrawScreen()
    {
        do
        {
            if (ClearScreenWhenRedraw)
            {
                Clear();
            }

            DrawWindow();
            SetCursorPosition(ContentLeft, ContentTop);
            DrawContent();
            Views.ForEach(v => { v.Draw(); });
        } while (ManageInput());
    }

    private void DrawWindow()
    {
        SetCursorPosition(Left, Top);
        for (var i = 0; i < Height; i++)
        {
            for (var j = 0; j < Width; j++)
            {
                if (i == 0 || i == Height - 1)
                {
                    Write("-");
                }
                else if (j == 0 || j == Width - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPositionToWritingLine();
        }
    }

    protected abstract void DrawContent();

    /** use this method when called WriteLine after. **/
    protected void SetCursorPositionToWritingLine(int addedLeft = 0, int addedTop = 0)
    {
        SetCursorPosition(addedLeft + Left + 1, addedTop + CursorTop);
    }

    protected virtual void DrawCommands()
    {
        
    }

    protected void SetCursorPositionToContentBlock()
    {
        SetCursorPosition(ContentLeft, ContentTop);
    }

    protected abstract bool ManageInput();
}