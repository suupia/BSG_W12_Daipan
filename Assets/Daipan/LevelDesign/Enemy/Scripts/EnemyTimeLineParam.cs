#nullable enable
using System;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
 
    [Serializable]
    public sealed class EnemyTimeLineParam
    {
        [Header("この区間の開始時刻（ゲーム開始を0とする）")]
        [Min(0)]
        public float startTime = 0f;

        [Header("エネミー生成のクールタイム")]
        [Min(0)]
        public float spawnDelaySec = 1f;

        [Header("エネミーの移動速度変化率（通常に対してx倍される）")]
        [Min(0)]
        public float moveSpeedRate = 1f;

        [Header("Bossの出現確率(n回敵を倒してもBossが出現しなかったら強制召喚)")]
        [Min(0)]
        public int spawnBossAmount = 10;

        [Header("Bossの出現確率(0%～100%)")]
        [Range(0f, 100f)]
        public float spawnBossRatio = 10f;
    }
   
}
