using Tools;
using UnityEngine;

namespace Scrips.Buffs
{
    public class BuffBase: MonoBehaviour, IBuff
    {
        public EnumTools.BuffName BuffName;
        public IHurtAble HurtAble;
        protected int maxLayer;
        public int currentLayer = 0;
        public virtual void OnBuffApplied(IHurtAble hurtAble, IBuffAble buffAble)
        {
            
        }

        public virtual void UpdateBuff()
        {
            
        }

        public virtual void OnBuffEnd(IHurtAble hurtAble, IBuffAble buffAble)
        {
            
        }

        public EnumTools.BuffName GetBuffName()
        {
            return BuffName;
        }
    }
}