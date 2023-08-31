using ZzabDenRing.Di;
using static System.Console;

namespace ZzabDenRing.Screens.CreateCharacter
{
    internal class CreateCharacterScreen : BaseScreen
    {
        private CreateCharViewModel _vm;
        private Action _navToMain;
        private Action _popBackStack;

        private const int ContentWidth = 60;
        private const int ContentHeight = 20;
        private const int ContentPadding = 8;

        private CreateStep[] _steps = Enum.GetValues<CreateStep>();
        private int NameInputLeft => Width / 2 - ContentWidth / 2 + 10 + ContentPadding;
        private int NameInputTop => Height / 6 + 1;


        public CreateCharacterScreen(
            Action navToMain,
            Action popBackStack
        )
        {
            ClearScreenWhenRedraw = true;
            _popBackStack = popBackStack;
            _navToMain = navToMain;
            _vm = new CreateCharViewModel(Container.GetRepository());
            _vm.State.Subscribe(state => { HandleState(state); });
        }

        protected override void DrawContent()
        {
            //여기서 캐릭터 이름과 직업을 선택함
            //validation 체크를 함
            var left = Width / 2 - ContentWidth / 2 + 1;
            var top = Height / 6;
            DrawSteps(left, top);
        }

        private void DrawSteps(int left, int top)
        {
            this.DrawBorder(left, top, ContentWidth, ContentHeight);

            SetCursorPosition(left + ContentPadding, top + 1);

            foreach (var step in _steps)

            {
                switch (step)
                {
                    case CreateStep.Name:
                        DrawName();
                        break;
                    case CreateStep.Job:
                        DrawJob();
                        break;
                    case CreateStep.Stats:
                        DrawStats();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                for (var i = 0; i < 4; i++)
                {
                    WriteLine();
                }

                SetCursorPosition(left + ContentPadding, CursorTop);
            }
        }

        private async void HandleState(CreateState state)
        {
            await Task.Delay(20);
            Beep();

            switch (state.CreateStep)
            {
                case CreateStep.Name:
                    BackgroundColor = ConsoleColor.Red;
                    Write(state.ErrorMessage ?? "");
                    ResetColor();
                    break;
                case CreateStep.Job:
                    break;
                case CreateStep.Stats:
                    BackgroundColor = ConsoleColor.Red;
                    Write(state.ErrorMessage ?? "");
                    ResetColor();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DrawName()
        {
            Write("이름 : ");
            Write(_vm.CurrentState.Name);
        }

        private void DrawJob()
        {
            Write("직업 : ");
            var startLeft = CursorLeft;
            var startTop = CursorTop;

            for (var i = 0; i < Constants.Jobs.Length; i++)
            {
                if (i == _vm.CurrentState.SelectedJobIdx && _vm.CurrentState.CreateStep == CreateStep.Job)
                    ForegroundColor = ConsoleColor.Blue;
                SetCursorPosition(startLeft + i * 15, startTop);
                var job = Constants.Jobs[i];
                this.DrawBorder(startLeft + i * 15, startTop, width: job.LengthToDisplay() + 4, height: 3);
                SetCursorPosition(
                    left: startLeft + i * 15 + (job.LengthToDisplay() + 4) / 2 - job.LengthToDisplay() / 2,
                    top: startTop + 1);
                Write(job);
                ResetColor();
            }
        }

        private void DrawStats()
        {
            var left = CursorLeft;
            var statHeader = "남은 스탯 : ";
            Write(statHeader);
            WriteLine($"{_vm.CurrentState.Stats}");

            var lines = new[]
            {
                $"Hp : {_vm.CurrentState.MaxHp}",
                $"Atk : {_vm.CurrentState.Atk}",
                $"Def : {_vm.CurrentState.Def}",
                $"Cri : {_vm.CurrentState.Cri}"
            };
            for (int i = 0; i < lines.Length; i++)
            {
                SetCursorPosition(left, CursorTop);
                if (i == _vm.CurrentState.CurStatIdx && _vm.CurrentState.CreateStep == CreateStep.Stats)
                {
                    ForegroundColor = ConsoleColor.Blue;
                    Write("==> ");
                }
                else
                {
                    Write("    ");
                }

                WriteLine(lines[i]);
                ResetColor();
            }
        }

        private void ReadName()
        {
            SetCursorPosition(NameInputLeft, NameInputTop);
            var name = ReadLine();
            _vm.OnNameChanged(name);
        }

        private void ReadJob()
        {
            SetCursorPosition(NameInputLeft - 10, NameInputTop + 2);
            Write("<- Press Enter To Select ->");
            var key = ReadKey();
            var command = key.Key switch
            {
                ConsoleKey.LeftArrow => Command.MoveLeft,
                ConsoleKey.RightArrow => Command.MoveRight,
                ConsoleKey.Enter => Command.Interaction,
                _ => Command.Nothing
            };
            _vm.OnJobChanged(command);
        }

        private void ReadStats()
        {
            SetCursorPosition(NameInputLeft - 10, NameInputTop + 7);
            Write("<- Decrease / Increase ->");
            var key = ReadKey();

            var command = key.Key switch
            {
                ConsoleKey.LeftArrow => Command.MoveLeft,
                ConsoleKey.RightArrow => Command.MoveRight,
                ConsoleKey.DownArrow => Command.MoveBottom,
                ConsoleKey.UpArrow => Command.MoveTop,
                ConsoleKey.Enter => Command.Interaction,
                _ => Command.Nothing
            };

            _vm.OnStatChanged(command);
        }

        protected override bool ManageInput()
        {
            switch (_vm.CurrentState.CreateStep)
            {
                case CreateStep.Name:
                    ReadName();
                    break;
                case CreateStep.Job:
                    ReadJob();
                    break;
                case CreateStep.Stats:
                    ReadStats();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return true;
        }
    }


    internal enum CreateStep
    {
        Name,
        Job,
        Stats
    }
}