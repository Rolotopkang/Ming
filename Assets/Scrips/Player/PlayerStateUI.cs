using System;
using TMPro;
using Tools;
using UnityEngine;

public class PlayerStateUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public TextMeshProUGUI Money;
    
    private void Update()
    {
            hp.text = Player.GetInstance().CurrentHP.ToString();
            Money.text = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Money).ToString();
    }
}
