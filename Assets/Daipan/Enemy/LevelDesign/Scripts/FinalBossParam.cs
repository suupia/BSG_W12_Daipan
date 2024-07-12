#nullable enable
using System;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [Serializable]
    public sealed class FinalBossParam
    {
        [Header("通常の敵と共通するパラメータを設定してください。")]
        public EnemyParam enemyParam = null!;

        [Header("FinalBOSS固有のパラメータを設定してください。")]
        public double summonActionIntervalSec = 1;

        public double summonEnemyIntervalSec = 1;
        public int summonEnemyCount = 5;
    }

}

