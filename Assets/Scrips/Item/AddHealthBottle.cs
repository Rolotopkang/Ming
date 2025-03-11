using Autohand;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AddHealthBottle : ItemBase
{
    public async void TryAddHealthBottle()
    {
        Player.GetInstance().CurrentHealthBottleCount += 2;
        await UniTask.WaitForSeconds(0.1f);
        Grabbable t = GetComponent<Grabbable>();
        t.ForceHandRelease(t.heldBy[0]);
        Destroy(gameObject);
    }
}
