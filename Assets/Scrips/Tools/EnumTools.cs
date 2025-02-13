namespace Tools
{
    public class EnumTools
    {
        public enum PlayerStatType
        {
            Attack,
            Health,
            Critical,
            Armor,
            Speed, 
            BulletCount,
            BulletSpread,
        }
        
        public enum EffectName
        {
            ShowTrajectoryLine,
            ExplosionBullet,
        }
        
        public enum GameEvent
        {
            BulletHit,
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