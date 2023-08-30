using ZzabDenRing.Data;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.CreateCharacter;

public class CreateCharViewModel
{
    private Repository _repository;
    private Character c;
    
    
    public CreateCharViewModel(Repository repository)
    {
        _repository = repository;
    }
    
    
}