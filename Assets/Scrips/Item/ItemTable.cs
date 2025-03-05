using System;
using Tools;
using UnityEngine;

public class ItemTable : Singleton<ItemTable>
{
    public int currentSlotNum = 0;
    private ItemSlot[] _itemSlots;

    protected override void Awake()
    {
        base.Awake();
        _itemSlots = GetComponentsInChildren<ItemSlot>();

    }

    private void Start()
    {
        currentSlotNum = (int)PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.ItemTable_SlotNum);
        for (int i = currentSlotNum; i < _itemSlots.Length; i++)
        {
            _itemSlots[i].gameObject.SetActive(false);
        }
    }

    public void BuySlot()
    {
        if (currentSlotNum>=12)
        {
            Debug.Log("不能再买了");
            return;
        }
        _itemSlots[currentSlotNum++].gameObject.SetActive(true);
    }

    public void updateNewPosition(Transform _transform)
    {
        transform.position = _transform.position;
    }
    
    
}
