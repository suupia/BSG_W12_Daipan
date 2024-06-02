#nullable enable
using System;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [Serializable]
    public class EnemyLevelDesignParam
    {
        [Header("BOSSの生成周期 (n回雑魚敵を倒したら生成)")]
        [Min(0)]
        public int spawnBossAmount;
        
        [Header("BOSS出現時に増加するイライラ度の量")]
        [Min(0)]
        public int increaseIrritatedValueByBoss;

        [Header("現在の雑魚敵の倒した数（自動更新されます）")]
        public int currentKillAmount;
    } 
}

