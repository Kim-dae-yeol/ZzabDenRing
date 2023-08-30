using ZzabDenRing.Di;
using static System.Console;

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
        //여기서 캐릭터 이름과 직업을 선택함 
        //validation 체크를 함

        _vm.CreateCharacter();
    }

    protected override bool ManageInput()
    {
        _vm.Message.Subscribe((error) => { WriteLine(error.ErrorMessage); });
        //todo 뒤로가기 이름 -> 직업선택 이후 생성
        _popBackStack();
        Thread.Sleep(500);
        var key = ReadKey();
        return true;
    }
}