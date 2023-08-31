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
        private const int ContentHeight = 15;
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

                WriteLine();
                SetCursorPosition(left + ContentPadding, CursorTop);
            }
        }

        private void HandleState(CreateState state)
        {
            switch (state.CreateStep)
            {
                case CreateStep.Name:
                    break;
                case CreateStep.Job:
                    break;
                case CreateStep.Stats:
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
        }

        private void DrawStats()
        {
            Write($"남은 스탯 : {_vm.CurrentState.Stats}");
        }

        private void ReadName()
        {
            SetCursorPosition(NameInputLeft, NameInputTop);
            var name = ReadLine();
            _vm.OnNameChanged(name);
        }

        protected override bool ManageInput()
        {
            switch (_vm.CurrentState.CreateStep)
            {
                case CreateStep.Name:
                    ReadName();
                    break;
                case CreateStep.Job:
                    break;
                case CreateStep.Stats:
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