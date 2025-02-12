using Tools;
using UnityEngine;

namespace Scrips.Effects
{
    public class ShowTrajectoryLineEffect : EffectBase
    {
        public override void ApplyEffect()
        {
            WeaponController.GetInstance().ShowSlingShotTrajectory = true;
            base.ApplyEffect();
        }

        public override void RemoveEffect()
        {
            WeaponController.GetInstance().ShowSlingShotTrajectory = false;
            base.RemoveEffect();
        }
        
    }
}