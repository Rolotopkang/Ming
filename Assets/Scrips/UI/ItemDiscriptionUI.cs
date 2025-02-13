using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionUI : UI_Base
{
    public TextMeshProUGUI ItemnName;
    public TextMeshProUGUI ItemDiscription;
    public TextMeshProUGUI Level;
    public Image ItemImage;

    private ItemBase _itemBase;
    

    public void ItemDescriptionUIRegister(ItemBase itemBase)
    {
        _itemBase = itemBase;
    }

    public override void UpdateUI()
    {
        if (_itemBase == null)
        {
            return;
        }
        ItemnName.text = _itemBase.ItemData.itemName;
        ItemnName.color = _itemBase.GetItemColor();
        ItemImage.sprite = _itemBase.ItemData.icon;
        ItemDiscription.text = _itemBase.DiscriptionToString();
        Level.text = _itemBase.ItemCount.ToString();
        Level.color = _itemBase. GetItemColor();
    }
}