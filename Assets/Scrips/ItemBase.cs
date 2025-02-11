using System;
using System.Text.RegularExpressions;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;


public class ItemBase : MonoBehaviour
{
    public ItemData ItemData;
    public int ItemCount = 1;
    private GameObject _discriptionUI;
    private Outline _outline;
    private int _showUICounter = 0;

    private void Start()
    {
        _discriptionUI = transform.GetChild(0).gameObject;
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
        _discriptionUI.GetComponent<ItemDescriptionUI>().ItemDescriptionUIRegister(this);
    }

    public bool CheckCanUpgrade()
    {
        return ItemCount < ItemData.MaxLevel;
    }

    public void UpgradeItem(ItemBase CommingItemBase)
    {
        if (ItemData.isEventItem)
            return;
        if (ItemCount >= ItemData.MaxLevel)
        {
            return;
        }
        
        ItemCount += CommingItemBase.ItemCount;
        if (ItemCount>=ItemData.MaxLevel)
        {
            ItemCount = ItemData.MaxLevel;
        }
        
        _discriptionUI.GetComponent<ItemDescriptionUI>().UpdateUI();
        UpdateOutLineColor();
    }

    public String DiscriptionToString()
    {
        return Regex.Replace(ItemData.description, @"{(\w+)}", match =>
        {
            string varName = match.Groups[1].Value;
            return varName switch
            {
                "BulletCount" => PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletCount)
                    .ToString(),
                "1" => "TEST",
                _ => match.Value
            };
        });
    }

    private void UpdateOutLineColor()
    {
        _outline.OutlineColor = GetItemColor();
    }

    private void CheckShowDiscriptionUI()
    {
        if (_showUICounter > 0)
        {
            _discriptionUI.SetActive(true);
            _outline.enabled = true;
        }
        else
        {
            _discriptionUI.SetActive(false);
            _outline.enabled = false;
        }
    }

    public void AddShowDiscriptionUI()
    {
        _showUICounter++;
        CheckShowDiscriptionUI();
    }

    public void DecreaseShowDiscriptionUI()
    {
        _showUICounter--;
        if (_showUICounter < 0)
        {
            _showUICounter = 0;
        }
        CheckShowDiscriptionUI();
    }
    
    public Color GetItemColor()
    {
        if (ItemData.isEventItem)
        {
            return Color.green;
        }
        else
        {
            return ItemCount switch
            {
                1 => Color.white ,
                2 => Color.blue ,
                3 => Color.magenta ,
                >=4 => Color.yellow,
                _ => Color.white // default case
            };
        }
    }
}