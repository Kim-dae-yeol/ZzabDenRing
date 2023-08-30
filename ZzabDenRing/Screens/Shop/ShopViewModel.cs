using ZzabDenRing.Data;
using ZzabDenRing.Di;
using ZzabDenRing.Model;

namespace ZzabDenRing.Screens.Shop;

public class ShopViewModel
{
    private ShopState _state;
    private readonly Repository _repository;

    public int CurrentShopIdx => _state.CurShopItemIdx;

    public int CurrentShopCursorIdx => _state.CurShopItemIdx >= ShopScreen.ItemRows
        ? ShopScreen.ItemRows - 1
        : _state.CurShopItemIdx;

    public int CurrentInventoryIdx => _state.CurInventoryItemIdx;

    public int CurrentInventoryCursorIdx => _state.CurInventoryItemIdx >= ShopScreen.ItemRows
        ? ShopScreen.ItemRows - 1
        : _state.CurInventoryItemIdx;

    public int TotalShopItems => _state.SellingItems.Count;
    public int TotalInventoryItems => _state.Inventory.Count;

    // todo where 문 추가하기 type 을 넣어서 ... !!  + skip + take 적용하기
    // 로직 생각하고 다시 정리하자. 장비창에서 했던 것과 같은 기능이니까 한번 다시 보고 정리 !!
    public IReadOnlyList<IItem> ShopItems => _state.SellingItems;

    public int CurX => _state.CurX;
    public int CurY => _state.CurY;

    public int CurrentInventoryTab => _state.CurrentInventoryTab;


    public int CurrentShopTab => _state.CurrentShopTab;

    private int SkipInventory =>
        CurrentInventoryIdx >= ShopScreen.ItemRows ? CurrentInventoryIdx - ShopScreen.ItemRows : 0;

    public IEnumerable<IItem> CurrentPageInventoryItems => _state.Inventory
        .Skip(SkipInventory)
        .Take(ShopScreen.ItemRows);

    private readonly int _shopTabLength = Enum.GetValues<ShopScreen.ShopTabs>().Length;
    private readonly int _invnetoryTabLength = Enum.GetValues<ShopScreen.InventoryTabs>().Length;

    private ShopScreen.ShopTabs ShopFilterType => (ShopScreen.ShopTabs)_state.CurrentShopTab;
    private ShopScreen.InventoryTabs InventoryFilterType => (ShopScreen.InventoryTabs)_state.CurrentInventoryTab;

    public bool IsInShopWindow => _state.CurY > 0 && CurX < _shopTabLength;
    public bool IsInInventoryWindow => _state.CurY > 0 && CurX >= _shopTabLength;
    internal ShopScreen.ShopTabs[] ShopTabItems = Enum.GetValues<ShopScreen.ShopTabs>();

    public bool IsEnhancementTab => _state.CurrentShopTab == (int)ShopScreen.ShopTabs.Enhancement;

    // todo 1. 강화 탭인 경우 강화 할 아이템이 선택됐는지 ?
    // todo 1. 탭 마다 필터링 적용 하기
    public string? Message;

    public ShopViewModel()
    {
        _repository = Container.GetRepository();

        _state = new(
            SellingItems: Game.Items.Select(it => it as IItem).ToList(),
            Inventory: _repository.Character.Inventory.Select(it => it as IItem).ToList(),
            CurX: 0,
            CurY: 0,
            CurrentShopTab: 0,
            CurShopItemIdx: 0,
            CurInventoryItemIdx: 0,
            CurrentInventoryTab: 0,
            Gold: _repository.Character.Gold
        );
    }

    public void OnCommand(Command cmd)
    {
        switch (cmd)
        {
            case Command.MoveTop:
                if (IsInShopWindow && _state.CurShopItemIdx > 0)
                {
                    _state = _state with { CurShopItemIdx = _state.CurShopItemIdx - 1 };
                }
                else if (IsInInventoryWindow && _state.CurInventoryItemIdx > 0)
                {
                    _state = _state with { CurInventoryItemIdx = _state.CurInventoryItemIdx - 1 };
                }
                else if (_state.CurY > 0)
                {
                    _state = _state with { CurY = _state.CurY - 1 };
                }

                break;
            case Command.MoveRight:
                if (IsInShopWindow)
                {
                    _state = _state with { CurX = _shopTabLength };
                }
                else if (_state.CurX < _shopTabLength + _invnetoryTabLength - 1)
                {
                    _state = _state with { CurX = _state.CurX + 1 };
                }


                break;
            case Command.MoveBottom:
                if (_state.CurY == 0)
                {
                    _state = _state with { CurY = _state.CurY + 1 };
                }
                else if (IsInInventoryWindow && _state.CurInventoryItemIdx < _state.Inventory.Count - 1)
                {
                    _state = _state with { CurInventoryItemIdx = _state.CurInventoryItemIdx + 1 };
                }
                else if (IsInShopWindow && _state.CurShopItemIdx < _state.SellingItems.Count - 1)
                {
                    _state = _state with { CurShopItemIdx = _state.CurShopItemIdx + 1 };
                }

                break;
            case Command.MoveLeft:
                if (_state.CurX > 0)
                {
                    _state = _state with { CurX = _state.CurX - 1 };
                }

                break;
            case Command.Interaction:
                if (CurY == 0)
                {
                    if (CurX < _shopTabLength)
                    {
                        if (CurX == (int)ShopScreen.ShopTabs.Enhancement)
                        {
                            _state = _state with { CurrentShopTab = CurX };
                        }
                        else
                        {
                            _state = _state with { CurrentShopTab = CurX };
                        }
                    }
                    else
                    {
                        _state = _state with { CurrentInventoryTab = CurX - _shopTabLength };
                    }

                    OnTabChanged();
                }
                else
                {
                    if (IsInInventoryWindow)
                    {
                        SellItem();
                    }
                    else if (IsInShopWindow)
                    {
                        BuyItem();
                    }
                }

                break;
        }
    }

    public void ConsumeMessage()
    {
        Message = null;
    }

    private void BuyItem()
    {
        Message = "아직 구현중이에영";
    }

    private void SellItem()
    {
        Message = "아직 구현중이에영";
    }

    private void EnhanceItem()
    {
        Message = "아직 구현중이에영";
    }

    private void OnTabChanged()
    {
        _state = _state with
        {
            Inventory = _repository.Character.Inventory
                .Select(it =>
                {
                    IItem item = it;
                    return item;
                }).Where(item =>
                {
                    switch (InventoryFilterType)
                    {
                        case ShopScreen.InventoryTabs.Equip:
                            return item is EquipItem;
                        case ShopScreen.InventoryTabs.Use:
                            return item is UseItem;
                        case ShopScreen.InventoryTabs.Material:
                            return item is MaterialItem;
                        default:
                            return false;
                    }
                }).ToList(),
            SellingItems = _repository.Character.Inventory
                .Select(it =>
                {
                    IItem item = it;
                    return item;
                })
                .Where(item =>
                {
                    switch (ShopFilterType)
                    {
                        case ShopScreen.ShopTabs.Equip:
                            return item is EquipItem;
                        case ShopScreen.ShopTabs.Use:
                            return item is UseItem;
                        case ShopScreen.ShopTabs.Material:
                            return item is MaterialItem;
                        default:
                            return false;
                    }
                }).ToList()
        };
    }
}

public record ShopState(
    List<IItem> SellingItems,
    List<IItem> Inventory,
    int CurX,
    int CurY,
    int CurrentShopTab,
    int CurrentInventoryTab,
    int CurShopItemIdx,
    int CurInventoryItemIdx,
    int Gold
)
{
}