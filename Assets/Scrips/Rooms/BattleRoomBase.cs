using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Tools;
using UnityEngine;

namespace Scrips
{
    public class BattleRoomBase : RoomBase
    {

        public Transform currentRewardPickPos;
        public Vector2 spawnAreaSize = new Vector2(10f, 10f);
        [Header("Gizmo Settings")]
        public Color spawnAreaColor = new Color(0f, 0f, 1f, 0.3f); // 正方形边框颜色
        public Color validPointColor = Color.green; // 合法点颜色
        public Color invalidPointColor = Color.red; // 非法点颜色
        
        
        private void OnDrawGizmos() {
            // 绘制刷怪区域边框
            Gizmos.color = spawnAreaColor;
            Vector3 center = transform.position;
            Vector3 size = new Vector3(spawnAreaSize.x, 0.1f, spawnAreaSize.y);
            Gizmos.DrawWireCube(center, size);
        }

        protected override async void OnLevelStart(Dictionary<string, object> args)
        {
            Debug.Log("战斗关卡开始");
            EnemySpawnManager.GetInstance().SpawnEnemy(spawnAreaSize,transform.position);
            await UniTask.WaitUntil(EnemySpawnManager.GetInstance().isWaveEnd);
            EventCenter.Publish(EnumTools.GameEvent.LevelEnd,null);
        }

        protected override void OnLevelEnd(Dictionary<string, object> args)
        {
            Debug.Log("战斗关卡结束");
            ItemTable.GetInstance().updateNewPosition(currentItemTablePos);
            RewardPickTable.GetInstance().updateNewPosition(currentRewardPickPos);
            RewardPickTable.GetInstance().ShowReward();
        }
    }
}