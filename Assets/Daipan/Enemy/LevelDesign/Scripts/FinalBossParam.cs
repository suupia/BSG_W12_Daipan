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

        [Header("召喚の間隔")][Min(0)]
        public double summonActionIntervalSec = 1;
        [Header("召喚する敵の数")][Min(0)]
        public int summonEnemyCount = 5;
        [Header("召喚時の敵が出現する間隔")][Min(0)]
        public double summonEnemyIntervalSec = 1;
        
        [Header("台パン時のダメージの割合")][Range(0, 100)]
        public double daipanHitDamagePercent = 10;
        [Header("ノックバック距離")][Min(0)]
        public double knockBackDistance = 1;

        [Header("倒したときに生成されるコメントの数")] [Min(0)] 
        public int commentCount = 5;
        
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

