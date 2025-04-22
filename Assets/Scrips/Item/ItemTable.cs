using System;
using System.Collections.Generic;
using Autohand;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

public class ItemTable : Singleton<ItemTable>
{
    public int currentSlotNum = 0;
    public GameObject BuyButton;
    public Transform HintPoint;
    private ItemSlot[] _itemSlots;
    
    
    

    protected override void Awake()
    {
        base.Awake();
        _itemSlots = GetComponentsInChildren<ItemSlot>();

    }

    private void OnEnable()
    {
        EventCenter.Subscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
    }
    private void OnDisable()
    {
        EventCenter.Unsubscribe(EnumTools.GameEvent.LevelStart,OnLevelStart);
    }
    

    private void Start()
    {
        currentSlotNum = (int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ItemTable_SlotNum);
        for (int i = currentSlotNum; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].gameObject.SetActive(false);
        }
    }

    private void OnLevelStart(Dictionary<String, object> args)
    {
        if (args.ContainsKey("RoomKind"))
        {
            if ((EnumTools.RoomKind)args["RoomKind"] == EnumTools.RoomKind.Store || (EnumTools.RoomKind)args["RoomKind"] == EnumTools.RoomKind.Boss)
            {
                BuyButton.SetActive(true);
            }
            else
            {
                BuyButton.SetActive(false);
            }
        }
    }

    public void BTN_BuySlot()
    {
        BuySlot(ShowHint);
    }

    public void ShowHint(int index)
    {
        switch (index)
        {
            case 0:
                HintManager.GetInstance().ShowHint("The slot is already maxed out！", 4, HintPoint);
                break;
            case 1:
                HintManager.GetInstance().ShowHint("Upgrade Successfully！", 4, HintPoint);
                break;
            case 2:
                HintManager.GetInstance().ShowHint("Need enough gold coins to upgrade! (15)", 4, HintPoint);
                break;
        }
        
        
    }

    /// <summary>
    /// 买槽位
    /// </summary>
    /// <param name="callback">0不能再买了、1成功、2没钱买了</param>
    private void BuySlot(Action<int> callback)
    {
        
        if (currentSlotNum>=12)
        {
            callback.Invoke(0);
            return;
        }

        if (Player.GetInstance().TryBuy(10f))
        {
            _itemSlots[currentSlotNum++].gameObject.SetActive(true);
            callback.Invoke(1); 
            return;
        }
        callback.Invoke(2);
    }

    public void ResetPlayer()
    {
        foreach (ItemSlot _itemSlot in _itemSlots)
        {
            Grabbable grabbable = _itemSlot._placePoint.placedObject;
            _itemSlot._placePoint.Remove();
            if (grabbable!=null)
            {
                grabbable.DoDestroy();
            }
            _itemSlot.gameObject.SetActive(false);
        }
        
        _itemSlots[0].gameObject.SetActive(true);
        _itemSlots[1].gameObject.SetActive(true);
        _itemSlots[2].gameObject.SetActive(true);
        _itemSlots[3].gameObject.SetActive(true);
    }
    
    public async void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
        transform.rotation = _transform.rotation;
    }
    
    
}
