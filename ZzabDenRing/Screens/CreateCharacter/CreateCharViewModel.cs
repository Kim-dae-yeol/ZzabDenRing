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
        null
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
            _state.Post(CurrentState with { ErrorMessage = "이름이 너무 깁니다." });
        }
        else
        {
            CurrentState = CurrentState with
            {
                Name = name,
                CreateStep = (CreateStep)((int)CurrentState.CreateStep + 1)
            };
        }
    }

    private void Validation()
    {
    }

    private void NextStep()
    {
    }

    private void CreateCharacter()
    {
        Validation();

        Character c = new Character(
            CurrentState.Name,
            "무직백수",
            200,
            200,
            20,
            1,
            30,
            1500,
            5,
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
    string? ErrorMessage
);