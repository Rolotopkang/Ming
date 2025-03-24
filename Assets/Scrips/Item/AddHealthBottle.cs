    using Autohand;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AddHealthBottle : ItemBase
{
    public async void TryAddHealthBottle()
    {
        Player.GetInstance().CurrentHealthBottleCount += 2;
        if (Player.GetInstance().CurrentHealthBottleCount <= 2)
        {
            HealingBottle.GetInstance().OnCandyTaken();
        }
        await UniTask.WaitForSeconds(0.1f);
        Grabbable t = GetComponent<Grabbable>();
        t.ForceHandRelease(t.heldBy[0]);
        Destroy(gameObject);
    }
}
