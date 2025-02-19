namespace Tools
{
    public class EnumTools
    {
        public enum PlayerStatType
        {
            Attack,
            AttackPercentage,
            Health,
            Critical,
            CriticalAmount,
            Armor,
            Speed, 
            BulletCount,
            BulletPenetrationCount,
            BulletSpread,
            ChainLightningDmgPercentage,
            ChainLightningMaxCount,
            ChainLightningRange,
            ExplosionRange,
            ExplosionDmgPercentage,
            Buff_Fire_MaxLayer,
            Buff_Fire_Duration,
            Buff_Fire_DmgPercentage,
            Buff_Poison_DmgPercentage,
            Buff_Poison_Duration,
            Buff_Ice_MaxLayer,
            Buff_Ice_SlowPercentage,
            Buff_Ice_Duration,
            
        }
        
        public enum EffectName
        {
            ShowTrajectoryLine,
            ExplosionBullet,
            ChainLightningBullet,
            Fire,
            Poison,
            Ice,
        }

        public enum BuffName
        {
            Fire,
            Ice,
            Frozen,
            Poison,
            Elite,
            ShieldArmed,
        }
        
        public enum GameEvent
        {
            BulletHit,
            BulletShot,
            PlayerHit,
            EnemyKilled
        }
        
        public enum DamageKind
        {
            Normal,
            Critical,
            Fire,
            Ice,
            Poison,
            Electrical,
            None
        }
    }
}