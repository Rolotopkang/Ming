using System;
using UnityEngine;

namespace Scrips.Enemy
{
    public class Dummy : EnemyBase
    {
        protected override void Start()
        {
            base.Start();
            InvokeRepeating(nameof(ResetHealth), 0f,1f);
        }

        private void ResetHealth()
        {
            CurrentHealth = EnemyData.MaxHPCurve.Evaluate(RoguelikeManager.GetInstance().layer);
            _enemyUIBase.UpdateUI();
        }
    }
}