using System.Collections.Generic;
using UnityEngine;

namespace Scrips.Managers
{
    
    [System.Serializable]
    public class EnemySpawnInfo {
        public GameObject enemyPrefab;
        public AnimationCurve countCurve;
    }
    
    [System.Serializable]
    [CreateAssetMenu(fileName = "NewWave", menuName = "Game/Wave")]
    public class Wave : ScriptableObject
    {
        public List<EnemySpawnInfo> enemySpawnInfos;
    }


}