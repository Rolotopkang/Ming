using Autohand;
using Cysharp.Threading.Tasks;
using Tools;

namespace Scrips.Item
{
    public class Money : ItemBase
    {
        public int Amount = 20;
        public async void TryAddMoney()
        {
            PlayerStatsManager.GetInstance().ApplyStatModifier(EnumTools.PlayerStatType.Money,Amount);
            await UniTask.WaitForSeconds(0.1f);
            Grabbable t = GetComponent<Grabbable>();
            t.ForceHandRelease(t.heldBy[0]);
            Destroy(gameObject);
        }
    }
}