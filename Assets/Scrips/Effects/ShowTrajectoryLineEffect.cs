using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class ShowTrajectoryLineEffect : EffectBase
    {
        public override void ApplyEffect()
        {
            var instance = WeaponController.GetInstance();
            if (instance != null) instance.ShowSlingShotTrajectory = true;
            base.ApplyEffect();
        }

        public override void RemoveEffect()
        {
            var instance = WeaponController.GetInstance();
            if (instance != null) instance.ShowSlingShotTrajectory = false;
            base.RemoveEffect();
        }
        
    }
}