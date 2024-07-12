#nullable enable
using System;
using UnityEngine;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [Serializable]
    public sealed class FinalBossParam
    {
        public double summonActionIntervalSec = 1;
        public double summonEnemyIntervalSec = 1;
        public int summonEnemyCount = 5;
    }

}

