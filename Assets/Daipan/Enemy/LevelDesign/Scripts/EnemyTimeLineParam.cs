#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [Serializable]
    public sealed class EnemyTimeLineParam
    {
        [Header("この区間の開始時刻（ゲーム開始を0とする）")] [Min(0)]
        public float startTime = 0f;

        [Header("エネミー生成のクールタイム")] [Min(0)] public float spawnIntervalSec = 1f;

        [Header("エネミーの移動速度変化率（通常に対してx倍される）")] [Min(0)]
        public double moveSpeedRate = 1f;

        [Header("Bossの出現確率(0%～100%)")] [Range(0f, 100f)]
        public double spawnBossPercent = 10f;
        
        [Header("Specialの出現確率(0%～100%)")] [Range(0f, 100f)]
        public double spawnSpecialPercent = 10f;
        
        [Header("Totemの出現確率(0%～100%)")] [Range(0f, 100f)]
        public double spawnTotemPercent = 10f;
    }
}