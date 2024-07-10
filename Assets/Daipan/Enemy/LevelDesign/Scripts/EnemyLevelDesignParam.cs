#nullable enable
using System;
using UnityEngine;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [Serializable]
    public sealed class EnemyLevelDesignParam
    {
        [Header("敵を倒した時に増加する視聴者数")] [Min(0)] 
        public int increaseViewerOnEnemyKill = 5;
        [Header("Special敵を倒した時に増加するイライラゲージの量")] [Min(0)] 
        public int increaseIrritationGaugeOnSpecialEnemyKill = 5;
        [Header("現在の雑魚敵の倒した数（自動更新されます）")] public int currentKillAmount;
    }
}