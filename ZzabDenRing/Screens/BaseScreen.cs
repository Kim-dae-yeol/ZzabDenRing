using ZzabDenRing.View;
using static System.Console;

namespace ZzabDenRing.Screens;

public abstract class BaseScreen : IScreen
{
    protected bool ClearScreenWhenRedraw = true;
    protected bool ShownCommands = false;
    public int Width = 150;
    public int Height = 30;
    public int Left = 0;
    public int Top = 0;
    public int CommandsHeight = 3;
    public int CommandLeft => Left + 1;
    public int CommandTop => Top + Height + 1;


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
            
            if (ShownCommands)
            {
                DrawCommandsWindow();
                DrawCommands();
            }
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
            SetCursorPosition(Left, CursorTop);
        }
    }

    private void DrawCommandsWindow()
    {
        SetCursorPosition(Left, Height);
        for (int i = 0; i < CommandsHeight; i++)
        {
            for (int j = 0; j < Width; j++)
            {
                if (i == 0 || i == CommandsHeight - 1)
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
            SetCursorPosition(Left, CursorTop);
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

    protected void SetCursorPositionToCommands()
    {
        SetCursorPosition(CommandLeft, CommandTop);
    }

    protected abstract bool ManageInput();
}