using System.Collections.Generic;
using UnityEngine;

namespace Scrips.Managers
{
    [CreateAssetMenu(fileName = "NewBigWave", menuName = "Game/BigWave")]
    public class BigWave : ScriptableObject
    {
        public bool waitDeath = true;
        public float spawnInterval = 20f;
        public List<Wave> bigWaveList;
    }
}