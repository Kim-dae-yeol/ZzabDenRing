using System.Reactive.Linq;
using System.Threading.Tasks.Dataflow;
using ZzabDenRing.Data;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.CreateCharacter;

public class CreateCharViewModel
{
    private const int MinAtk = 10;
    private const int MinMaxHp = 100;
    private const int MinDef = 10;
    private const int MinCri = 5;
    private const int MaxStats = 4;
    private Repository _repository;

    internal CreateState CurrentState { get; private set; } = new(
        CreateStep.Name,
        Name: "",
        SelectedJobIdx: 0,
        Stats: 10,
        Atk: MinAtk,
        Def: MinAtk,
        MaxHp: MinMaxHp,
        Cri: MinCri,
        CurStatIdx: 0,
        ErrorMessage: null
    );

    private BufferBlock<CreateState> _state = new();
    internal IObservable<CreateState> State => _state.AsObservable();

    public CreateCharViewModel(Repository repository)
    {
        _repository = repository;
    }

    public void OnNameChanged(string? name)
    {
        if (string.IsNullOrEmpty(name))
        {
            _state.Post(CurrentState with { ErrorMessage = "이름을 입력해주세요." });
        }
        else if (name.LengthToDisplay() > 16)
        {
            _state.Post(CurrentState with { ErrorMessage = "이름이 너무 깁니다. 8글자이내로 작성해주세요." });
        }
        else
        {
            CurrentState = CurrentState with
            {
                Name = name
            };
            NextStep();
        }
    }

    public void OnJobChanged(Command command)
    {
        if (command == Command.MoveLeft)
        {
            var idx = (CurrentState.SelectedJobIdx - 1 + Constants.Jobs.Length) % Constants.Jobs.Length;
            CurrentState = CurrentState with { SelectedJobIdx = idx };
        }
        else if (command == Command.MoveRight)
        {
            var idx = (CurrentState.SelectedJobIdx + 1) % Constants.Jobs.Length;
            CurrentState = CurrentState with { SelectedJobIdx = idx };
        }
        else if (command == Command.Interaction)
        {
            NextStep();
        }
    }

    public void OnStatChanged(Command command)
    {
        switch (command)
        {
            case Command.MoveLeft:
            {
                DecreaseCurrentStat();
                break;
            }
            case Command.MoveTop:
            {
                var idx = (CurrentState.CurStatIdx - 1 + MaxStats) % MaxStats;
                CurrentState = CurrentState with { CurStatIdx = idx };
                break;
            }
            case Command.MoveRight:
            {
                IncreaseCurrentStat();
                break;
            }
            case Command.MoveBottom:
            {
                var idx = (CurrentState.CurStatIdx + 1) % MaxStats;
                CurrentState = CurrentState with { CurStatIdx = idx };
                break;
            }
            case Command.Interaction:
                if (ValidateStats())
                {
                    NextStep();
                }
                else
                {
                    _state.Post(CurrentState with { ErrorMessage = "스탯을 다 사용해주세요." });
                }

                break;
        }
    }

    private void IncreaseCurrentStat()
    {
        //todo stat -> enum 으로 리팩토링 
        if (CurrentState.Stats <= 0)
        {
            _state.Post(CurrentState with { ErrorMessage = "스탯이 없습니다" });
            return;
        }

        switch (CurrentState.CurStatIdx)
        {
            case 0:
                CurrentState = CurrentState with { MaxHp = CurrentState.MaxHp + 50 };
                break;
            case 1:
                CurrentState = CurrentState with { Atk = CurrentState.Atk + 10 };
                break;
            case 2:
                CurrentState = CurrentState with { Def = CurrentState.Def + 10 };
                break;
            case 3:
                CurrentState = CurrentState with { Cri = CurrentState.Cri + 1 };
                break;
        }

        CurrentState = CurrentState with { Stats = CurrentState.Stats - 1 };
    }

    private void DecreaseCurrentStat()
    {
        //todo stat -> enum 으로 리팩토링 
        switch (CurrentState.CurStatIdx)
        {
            case 0:
                if (CurrentState.MaxHp == MinMaxHp)
                {
                    _state.Post(CurrentState with { ErrorMessage = "이미 최소치입니다." });
                    break;
                }

                CurrentState = CurrentState with { MaxHp = CurrentState.MaxHp - 50 };
                break;
            case 1:
                if (CurrentState.Atk == MinAtk)
                {
                    _state.Post(CurrentState with { ErrorMessage = "이미 최소치입니다." });
                    break;
                }

                CurrentState = CurrentState with { Atk = CurrentState.Atk - 10 };
                break;
            case 2:
                if (CurrentState.Def == MinDef)
                {
                    _state.Post(CurrentState with { ErrorMessage = "이미 최소치입니다." });
                    break;
                }

                CurrentState = CurrentState with { Def = CurrentState.Def - 10 };
                break;
            case 3:
                if (CurrentState.Cri == MinCri)
                {
                    _state.Post(CurrentState with { ErrorMessage = "이미 최소치입니다." });
                    break;
                }

                CurrentState = CurrentState with { Cri = CurrentState.Cri - 1 };
                break;
        }
    }

    private void NextStep()
    {
        CurrentState = CurrentState with
        {
            CreateStep = (CreateStep)((int)CurrentState.CreateStep + 1)
        };

        if (CurrentState.CreateStep == CreateStep.Exit)
        {
            CreateCharacter();
        }
    }

    private bool ValidateStats()
    {
        return CurrentState.Stats == 0;
    }


    private void CreateCharacter()
    {
        Character c = new Character(
            CurrentState.Name,
            Constants.Jobs[CurrentState.SelectedJobIdx],
            maxHp: CurrentState.MaxHp,
            hp: CurrentState.MaxHp,
            atk: CurrentState.Atk,
            level: 1,
            def: CurrentState.Def,
            Constants.StartingGold,
            CurrentState.Cri,
            new List<IItem>(),
            new Model.Equipment()
        );

        _repository.CreateCharacter(c);
    }
}

internal record CreateState(
    CreateStep CreateStep,
    string Name,
    int SelectedJobIdx,
    int Stats,
    int Atk,
    int Def,
    int MaxHp,
    int Cri,
    int CurStatIdx,
    string? ErrorMessage
);