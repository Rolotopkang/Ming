using System.Collections.Generic;

namespace Scrips
{
    public class ItemRoom : BattleRoomBase
    {
        protected override void OnLevelStart(Dictionary<string, object> args)
        {
            base.OnLevelStart(args);
        }

        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            RewardPickTable.GetInstance().updateNewPosition(currentRewardPickPos);
            RewardPickTable.GetInstance().ShowReward();
        }
    }
}