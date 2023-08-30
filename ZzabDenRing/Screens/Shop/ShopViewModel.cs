using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Shop;

public class ShopViewModel
{
    private ShopState _state;
    private readonly Repository _repository;
    public int CurrentShopIdx => _state.CurShopItemIdx;
    public int TotalShopItems => _state.SellingItems.Count;

    // todo where 문 추가하기 type 을 넣어서 ... !!  + skip + take 적용하기
    // 로직 생각하고 다시 정리하자. 장비창에서 했던 것과 같은 기능이니까 한번 다시 보고 정리 !!
    public IReadOnlyList<EquipItem> ShopItems => _state.SellingItems;

    public int CurX => _state.CurX;
    public int CurY => _state.CurY;

    public int CurrentInventoryTab => _state.CurrentInventoryTab;
    public int CurrentInventoryIdx => _state.CurInventoryItemIdx;
    public int CurrentShopTab => _state.CurrentShopTab;
    public int CurrentInventoryPage => 1;

    public int MaxInventoryPage => _state.Inventory.Count / ShopScreen.ItemRows +
                                   _state.Inventory.Count % ShopScreen.ItemRows;

    public IEnumerable<EquipItem> CurrentPageInventoryItems => _state.Inventory
        .Skip(0)
        .Take(ShopScreen.ItemRows);

    private readonly int _shopTabLength = Enum.GetValues<ShopScreen.ShopTabs>().Length;
    private readonly int _invnetoryTabLength = Enum.GetValues<ShopScreen.InventoryTabs>().Length;

    public bool IsInShopWindow => _state.CurY > 0 && CurX < _shopTabLength;
    public bool IsInInventoryWindow => _state.CurY > 0 && CurX >= _shopTabLength;

    public string? Message = null;
    
    public ShopViewModel()
    {
        
        _state = new(
            SellingItems: Game.Items.ToList(),
            Inventory: _repository.Character.Inventory,
            CurX: 0,
            CurY: 0,
            CurrentShopTab: 0,
            CurShopItemIdx: 0,
            CurInventoryItemIdx: 0,
            CurrentInventoryTab: 0,
            Gold: _repository.Character.Gold,
            CurInventoryPage: 1
        );
    }

    public void OnCommand(Command cmd)
    {
        switch (cmd)
        {
            case Command.MoveTop:
                if (_state.CurY > 0)
                {
                    _state = _state with { CurY = _state.CurY - 1 };
                }

                break;
            case Command.MoveRight:
                if (_state.CurX < _shopTabLength + _invnetoryTabLength - 1)
                {
                    _state = _state with { CurX = _state.CurX + 1 };
                }

                break;
            case Command.MoveBottom:
                if (_state.CurY == 0)
                {
                    _state = _state with { CurY = _state.CurY + 1 };
                }

                break;
            case Command.MoveLeft:
                if (_state.CurX > 0)
                {
                    _state = _state with { CurX = _state.CurX - 1 };
                }

                break;
            case Command.Interaction:
                Message = "아직 구현중이에요 ";
                break;
        }
    }

    public void ConsumeMessage()
    {
        Message = null;
    }

    private void BuyItem()
    {
    }

    private void SellItem()
    {
    }

    private void EnhanceItem()
    {
    }
}

public record ShopState(
    List<EquipItem> SellingItems,
    List<EquipItem> Inventory,
    int CurX,
    int CurY,
    int CurrentShopTab,
    int CurrentInventoryTab,
    int CurShopItemIdx,
    int CurInventoryItemIdx,
    int CurInventoryPage,
    int Gold
);