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

    public int CurrentInventoryIdx => _state.Inventory.Count == 0 ? -1 : _state.CurInventoryItemIdx;

    public int CurrentInventoryCursorIdx => _state.CurInventoryItemIdx >= ShopScreen.ItemRows
        ? ShopScreen.ItemRows - 1
        : _state.CurInventoryItemIdx;

    public int TotalShopItems => _state.SellingItems.Count;
    public int TotalInventoryItems => _state.Inventory.Count;

    public IReadOnlyList<IItem> ShopItems => _state.SellingItems;

    public int CurX => _state.CurX;
    public int CurY => _state.CurY;

    public int CurrentInventoryTab => _state.CurrentInventoryTab;


    public int CurrentShopTab => _state.CurrentShopTab;

    private int SkipInventory =>
        CurrentInventoryIdx >= ShopScreen.ItemRows ? CurrentInventoryIdx - ShopScreen.ItemRows + 1 : 0;

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
    public int Gold => _state.Gold;

    public EquipItem EnhanceSlotItem => _state.EnhanceItem;

    private int _enhancePercent => 100 - (EnhanceSlotItem.Enhancement * (int)(EnhanceSlotItem.Grade + 1) * 4);

    public int EnhancePercent
    {
        get
        {
            if (_enhancePercent > 100)
            {
                return 100;
            }

            return _enhancePercent switch
            {
                <= 5 => 5,
                _ => _enhancePercent
            };
        }
    }

    public bool Success { get; private set; }
    public string? Message;

    public int StoneCount => _repository.Character
        .Inventory
        .Count(it => it is MaterialItem);

    public int NeedStone => (int)(EnhanceSlotItem.Grade + 1) * EnhanceSlotItem.Enhancement + 1;
    public bool CanEnhance => StoneCount >= NeedStone;


    public ShopViewModel()
    {
        _repository = Container.GetRepository();
        LoadData();
    }

    private void LoadData()
    {
        var character = _repository.Character;
        var inventory = _repository.Character.Inventory.Where(it => it is EquipItem).ToList();
        var gold = character.Gold;

        _state = new(
            SellingItems: _repository.Shopper.SellingItems.Select(item => item as IItem).ToList(),
            Inventory: inventory,
            CurX: 0,
            CurY: 0,
            CurrentShopTab: 0,
            CurShopItemIdx: 0,
            CurInventoryItemIdx: 0,
            CurrentInventoryTab: 0,
            Gold: gold
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
                    if (CurrentShopTab == (int)ShopScreen.ShopTabs.Enhancement)
                    {
                        _state = _state with
                        {
                            CurY = _state.CurY + 1,
                            CurX = 4
                        };
                    }
                    else
                    {
                        _state = _state with { CurY = _state.CurY + 1 };
                    }
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
                    if (CurrentShopTab == (int)ShopScreen.ShopTabs.Enhancement)
                    {
                        AddItemToEnhanceSlot();
                    }
                    else if (IsInInventoryWindow)
                    {
                        SellItem();
                    }
                    else if (IsInShopWindow)
                    {
                        BuyItem();
                    }
                }

                break;
            case Command.Enhance:
                Enhance();
                break;
        }
    }

    public void ConsumeMessage()
    {
        Message = null;
    }

    private void BuyItem()
    {
        var selectedItem = _state.SellingItems.ElementAtOrDefault(CurrentShopIdx);
        if (selectedItem == null)
        {
            return;
        }

        if (selectedItem.Price > Gold)
        {
            Message = "골드가 부족합니다.";
        }
        else
        {
            Message = $"{selectedItem.Name}을(를) {selectedItem.Price}에 구매합니다.";
            _repository.BuyItem(selectedItem);

            _state = _state with
            {
                Inventory = _repository.Character.Inventory.Where(it =>
                    {
                        return CurrentInventoryTab switch

                        {
                            (int)ShopScreen.InventoryTabs.Equip => it is EquipItem,
                            (int)ShopScreen.InventoryTabs.Material => it is MaterialItem,
                            (int)ShopScreen.InventoryTabs.Use => it is UseItem,
                            _ => false
                        };
                    }
                ).ToList(),
                Gold = _repository.Character.Gold
            };
        }
    }

    private void SellItem()
    {
        var selectedItem = _state.Inventory.ElementAtOrDefault(CurrentInventoryIdx);
        if (selectedItem == null)
        {
            return;
        }

        Message = $"{selectedItem.Name}을(를) {selectedItem.Price * 3 / 10}에 판매합니다";
        _repository.SellItem(selectedItem);

        _state = _state with
        {
            Inventory = _repository.Character.Inventory.Where(it =>
                {
                    return CurrentInventoryTab switch

                    {
                        (int)ShopScreen.InventoryTabs.Equip => it is EquipItem,
                        (int)ShopScreen.InventoryTabs.Material => it is MaterialItem,
                        (int)ShopScreen.InventoryTabs.Use => it is UseItem,
                        _ => false
                    };
                }
            ).ToList(),
            Gold = _repository.Character.Gold
        };
    }

    private void AddItemToEnhanceSlot()
    {
        var selected = _state.Inventory[_state.CurInventoryItemIdx] as EquipItem;
        if (_state.EnhanceItem == selected)
        {
            _state = _state with { EnhanceItem = EquipItem.Empty };
        }
        else
        {
            _state = _state with { EnhanceItem = selected ?? EquipItem.Empty };
        }

        Message = "아직 구현중이에영";
    }

    private void OnTabChanged()
    {
        _state = _state with
        {
            CurInventoryItemIdx = 0, CurShopItemIdx = 0
        };
        if (CurrentShopTab == (int)ShopScreen.ShopTabs.Enhancement)
        {
            _state = _state with
            {
                CurrentInventoryTab = (int)ShopScreen.InventoryTabs.Equip,
                Inventory = _repository.Character.Inventory
                    .Where(it => it is EquipItem)
                    .ToList(),
                CurX = 4,
                CurY = 1
            };
            return;
        }


        var shopperItems = ShopFilterType switch
        {
            ShopScreen.ShopTabs.Equip => new List<IItem>(_repository.Shopper.SellingItems),
            ShopScreen.ShopTabs.Use => new List<IItem>(_repository.Shopper.SellingUse),
            ShopScreen.ShopTabs.Material => new List<IItem>(_repository.Shopper.SellingMaterial),
            ShopScreen.ShopTabs.Enhancement => throw new ArgumentOutOfRangeException(),
            _ => throw new ArgumentOutOfRangeException()
        };

        _state = _state with
        {
            SellingItems = shopperItems,
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
        };
    }

    private void Enhance()
    {
        if (StoneCount < NeedStone)
        {
            Message = "강화의 돌의 갯수가 부족합니다";
            return;
        }

        var success = Random.Shared.Next(1, 101) <= EnhancePercent;
        _repository.Enhance(NeedStone);
        Success = success;
        if (success)
        {
            EnhanceSlotItem.Enhance();
            Message = "성공! 축하합니다!";
        }
        else
        {
            Message = "실패했습니다...ㅠㅠ";
        }

        _repository.SaveData();
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
    public EquipItem EnhanceItem = EquipItem.Empty;
}