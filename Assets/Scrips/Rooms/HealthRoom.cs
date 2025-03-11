using System.Collections.Generic;

namespace Scrips
{
    public class HealthRoom : BattleRoomBase
    {
        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            RewardPickTable.GetInstance().updateNewPosition(currentRewardPickPos);
            RewardPickTable.GetInstance().ShowHealthReward();
        }
    }
}