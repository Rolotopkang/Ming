using Tools;
using UnityEngine;

namespace Scrips.Buffs
{
    public interface IHurtAble
    {
        public void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
        {
            
        }

        public void CheckDeath();

        public void Death();
        public float GetHealthPercent();

        public void Slow(float amount);

        public float GetMaxHealth();

        public Vector3 GetCenter();
    }
}