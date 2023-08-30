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
    public int CommandsWidth;
    public int CommandLeft;
    public int CommandTop;


    protected int ContentLeft => Left + 1;
    protected int ContentTop => Top + 1;

    protected List<IView> Views = new();

    protected BaseScreen()
    {
        CommandsWidth = Width;
        CommandLeft = Left + 1;
        CommandTop = Top + Height + 1;
    }

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
                SetCursorPositionToCommands();
                SetCursorPosition(CursorLeft + 1, CursorTop + 1);
                DrawCommands();
            }

            Views.ForEach(v => { v.Draw(); });
            SetCursorPositionToCommands();
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
        SetCursorPosition(CommandLeft, CommandTop);
        for (int i = 0; i < CommandsHeight; i++)
        {
            for (int j = 0; j < CommandsWidth; j++)
            {
                if (i == 0 || i == CommandsHeight - 1)
                {
                    Write("-");
                }
                else if (j == 0 || j == CommandsWidth - 1)
                {
                    Write("|");
                }
                else
                {
                    Write(" ");
                }
            }

            WriteLine();
            SetCursorPosition(CommandLeft, CursorTop);
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

    /** return  true : 화면에서 계속 머무름 , false : 화면 전환 */
    protected abstract bool ManageInput();
}