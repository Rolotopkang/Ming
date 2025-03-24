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
            ItemTable_SlotNum,
            ShotPower,
            Buff_Ice_FrozenDuration,
            Buff_Ice_DmgPercentage,
            GiantKillerPercentage,
            HealthBottleNum,
            
        }
        
        public enum EffectName
        {
            ShowTrajectoryLine,
            ExplosionBullet,
            ChainLightningBullet,
            Fire,
            Poison,
            Ice,
            TracingBullets,
            GiantKiller,
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
        
        public enum EnemyKind
        {
            Melee,
            Range,
            Elite,
            Boss,
        }
        
        public enum RoomKind
        {
            Item,
            Money,
            Health,
            Event,
            Store,
            Boss,
            Test,
            None,
        }
        
        public enum GameEvent
        {
            BulletHit,
            BulletShot,
            PlayerHit,
            EnemyKilled,
            LevelStart,
            LevelEnd,
            GameStart,
            PlayerDeath,
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