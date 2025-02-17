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
            ChainLightningRange
        }
        
        public enum EffectName
        {
            ShowTrajectoryLine,
            ExplosionBullet,
            ChainLightningBullet,
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