using System;
using System.Collections.Generic;
using Scrips.Buffs;
using Scrips.UI;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyUI_Base : UI_Base
{
    public TextMeshProUGUI EnemyName;
    public TextMeshProUGUI HealthCount;
    public Image HealthFill;
    public GameObject BuffRoot;
    public GameObject BuffIconPrefab;
    
    private EnemyBase _enemyBase;

    public void EnemyUIRegister(EnemyBase enemyBase)
    {
        _enemyBase = enemyBase;
    }

    public override void UpdateUI()
    {
        base.UpdateUI();
        if (_enemyBase == null)
        {
            return;
        }
        EnemyName.text = _enemyBase.EnemyData.EnemyName;
        HealthCount.text = string.Concat(_enemyBase.CurrentHealth.ToString("F1"), "/", _enemyBase.EnemyData.MaxHealth.ToString("F1"));
        HealthFill.fillAmount = _enemyBase.GetHealthPercent();
    }

    public void UpdateBuffUI()
    {
        foreach (Transform child in BuffRoot.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (BuffBase buffBase in _enemyBase.GetBuffList())
        { 
            UI_BuffIcon tmp_Icon = Instantiate(BuffIconPrefab, BuffRoot.transform).GetComponent<UI_BuffIcon>();
            tmp_Icon.Register(buffBase);
        }
    }
}
