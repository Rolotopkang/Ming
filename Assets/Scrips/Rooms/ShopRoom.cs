using System.Collections.Generic;
using UnityEngine;

namespace Scrips
{
    public class ShopRoom : RoomBase
    {
        public Transform currentUpgradeTabel;
        public Transform currentForgeTabel;
        
        protected override void OnLevelStart(Dictionary<string, object> args)
        {
            base.OnLevelStart(args);
        }

        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            ItemTable.GetInstance().updateNewPosition(currentItemTablePos);
            ItemUpgradeTabel.GetInstance().updateNewPosition(currentUpgradeTabel);
            ItemForgingTable.GetInstance().updateNewPosition(currentForgeTabel);
        }
    }
}