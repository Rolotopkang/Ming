using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyUI_Base : UI_Base
{
    public TextMeshProUGUI EnemyName;
    public TextMeshProUGUI HealthCount;
    public Image HealthFill;

    private EnemyBase _enemyBase;

    public void EnemyUIRegister(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
        Debug.Log("registerEnemy");
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        if (_enemyBase == null)
        {
            return;
        }
        EnemyName.text = _enemyBase.EnemyData.EnemyName;
        HealthCount.text = string.Concat(_enemyBase.CurrentHealth, "/", _enemyBase.EnemyData.MaxHealth);
        HealthFill.fillAmount = _enemyBase.GetHealthPercent();
    }
}
