using System.Reactive.Linq;
using System.Threading.Tasks.Dataflow;
using ZzabDenRing.Data;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.CreateCharacter;

public class CreateCharViewModel
{
    private Repository _repository;

    private BufferBlock<ErrorState> _message = new();
    public IObservable<ErrorState> Message => _message.AsObservable();

    public CreateCharViewModel(Repository repository)
    {
        _repository = repository;
    }

    private async void Validation()
    {
        for (var i = 1; i < 30; i++)
        {
            //await for 2 sec
            await Task.Delay(200);
            _message.Post(new("name", "hello"));
        }
    }

    public async void CreateCharacter()
    {
        Validation();
        Character c = new Character(
            "새로운 모험가",
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

        await _repository.CreateCharacter(c);
    }
}

public record ErrorState(
    string ErrorType,
    string ErrorMessage
);