using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Scrips
{
    public class BossRoom : RoomBase
    {
        public GameObject EndGameUI;
        public Transform currentUpgradeTabel;
        public Transform currentForgeTabel;
        
        protected override void OnLevelStart(Dictionary<string, object> args)
        {
            base.OnLevelStart(args);
            ItemTable.GetInstance().updateNewPosition(currentItemTablePos);
            ItemUpgradeTabel.GetInstance().updateNewPosition(currentUpgradeTabel);
            ItemForgingTable.GetInstance().updateNewPosition(currentForgeTabel);
            
        }

        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            
        }

        
        
        public async void ShowEndGameUI()
        {
            UniTask.WaitForSeconds(4f);
            EndGameUI.SetActive(true);
        }
    }
}