using ZzabDenRing.Di;

namespace ZzabDenRing.Screens.CreateCharacter;

public class CreateCharacterScreen : BaseScreen
{
    private CreateCharViewModel _vm;
    private Action _navToMain;
    private Action _popBackStack;

    public CreateCharacterScreen(
        Action navToMain,
        Action popBackStack
    )
    {
        _popBackStack = popBackStack;
        _navToMain = navToMain;
        _vm = new CreateCharViewModel(Container.GetRepository());
    }

    protected override void DrawContent()
    {
        throw new NotImplementedException();
    }

    protected override bool ManageInput()
    {
        //todo 뒤로가기 이름 -> 직업선택 이후 생성
        throw new NotImplementedException();
    }
}