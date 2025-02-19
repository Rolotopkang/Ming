using System.Collections.Generic;
using Tools;

namespace Scrips.Effects
{
    public class PoisonBulletEffect : EffectBase
    {
        public override void ApplyEffect()
        {
            EventCenter.Subscribe(EnumTools.GameEvent.BulletShot,OnBulletShot);
            base.ApplyEffect();
        }

        public override void RemoveEffect()
        {
            EventCenter.Unsubscribe(EnumTools.GameEvent.BulletShot,OnBulletShot);
            base.RemoveEffect();
        }
        
        private void OnBulletShot(Dictionary<string, object> arg)
        {
            BulletBase bulletBase = (BulletBase)arg["BulletBase"];
            bulletBase.gameObject.AddComponent<Poison>();
        }
    }
}