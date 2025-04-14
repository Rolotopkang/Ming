using Tools;
using UnityEngine;

public class boss : EnemyBase
{
    
    
    
    
    public override void TakeDamage(float dmg, EnumTools.DamageKind damageKind, Vector3 position)
    {
        base.TakeDamage(dmg, damageKind, position);
        animator.SetTrigger("hit");
    }
}
