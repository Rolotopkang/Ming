using System;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStateUI : MonoBehaviour
{
    public TextMeshProUGUI hp;
    public TextMeshProUGUI Money;
    public TextMeshProUGUI Layer;
    public TextMeshProUGUI Candy;
    public Image bar;
    
    private void Update()
    {
            hp.text = Player.GetInstance().CurrentHP.ToString();
            Money.text = PlayerStatsManager.GetInstance().GetStatValue(EnumTools.PlayerStatType.Money).ToString();
            Layer.text = RoguelikeManager.GetInstance().layer.ToString();
            Candy.text = Player.GetInstance().CurrentHealthBottleCount.ToString();
            bar.fillAmount = Player.GetInstance().GetHealthPercent();
    }
}
