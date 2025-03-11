using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    public class EventRoom : RoomBase
    {
        public Transform currentRewardPickPos;
        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            ItemTable.GetInstance().updateNewPosition(currentItemTablePos);
            RewardPickTable.GetInstance().updateNewPosition(currentRewardPickPos);
            RewardPickTable.GetInstance().ShowEventReward();
        }
    }
}