using System;
using System.Text.RegularExpressions;
using Tools;
using UnityEngine;
using UnityEngine.Rendering;

public class ItemBase : MonoBehaviour
{
    public ItemData ItemData;
    
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

    public String DiscriptionToString()
    {
        return Regex.Replace(ItemData.description, @"{(\w+)}", match =>
        {
            string varName = match.Groups[1].Value;
            return varName switch
            {
                "BulletCount" => PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.BulletCount).ToString(),
                "1" => "TEST",
                _ => match.Value // 未匹配到变量就保持原样
            };
        });
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
        CheckShowDiscriptionUI();
    }
}
