#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
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
        
        [Header("FinalBOSSの色のパラメータを設定してください。")]
        public List<FinalBossColorParam> finalBossColorParams = null!;
    }
    
    [Serializable]
    public sealed class FinalBossColorParam
    {
        public FinalBossColor finalBossColor;
        public Color bodyColor = Color.white;
        public Color eyeColor = new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color eyeBallColor = Color.white;
        public Color lineColor = new(111f / 255f, 87f / 255f, 107f / 255f);
    }
}

