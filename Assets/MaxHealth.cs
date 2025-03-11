using Autohand;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

public class MaxHealth : ItemBase
{
    public float HealthAmount = 50f;
    
    public async void AddMaxHealth()
    {
        PlayerStatsManager.GetInstance().ApplyStatModifier(EnumTools.PlayerStatType.Health,HealthAmount);
        Player.GetInstance().Healing(HealthAmount);
        await UniTask.WaitForSeconds(0.1f);
        Grabbable t = GetComponent<Grabbable>();
        t.ForceHandRelease(t.heldBy[0]);
        Destroy(gameObject);
    }
}
