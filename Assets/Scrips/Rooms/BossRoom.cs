using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Scrips
{
    public class BossRoom : RoomBase
    {
        public GameObject EndGameUI;
        public Transform currentUpgradeTabel;
        public Transform currentForgeTabel;
        public float ConvergenceBossRoomLayer = 12;
        
        protected override void OnLevelStart(Dictionary<string, object> args)
        {
            base.OnLevelStart(args);
            ItemTable.GetInstance().updateNewPosition(currentItemTablePos);
            // ItemUpgradeTabel.GetInstance().updateNewPosition(currentUpgradeTabel);
            ItemForgingTable.GetInstance().updateNewPosition(currentForgeTabel);
            //会场特供
            PlayerStatsManager.GetInstance().ApplyStatModifier(EnumTools.PlayerStatType.Money,9999);
            RoguelikeManager.GetInstance().layer = (int)ConvergenceBossRoomLayer;
        }

        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            base.OnLevelEnd(args);
            
        }

        
        
        public async void ShowEndGameUI()
        {
            await UniTask.WaitForSeconds(3f);
            EndGameUI.SetActive(true);
        }
    }
}