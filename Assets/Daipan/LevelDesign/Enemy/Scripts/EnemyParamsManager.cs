using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [CreateAssetMenu(fileName = "EnemyParamsManager", menuName = "ScriptableObjects/Enemy/EnemyParamsManager", order = 1)]
    public sealed class EnemyParamsManager : ScriptableObject
    {
        [Header("エネミー全体のレベルデザインはこちら")]
        [Space(30)]

        public EnemyLevelDesignParam enemyLevelDesignParam = null!;
        
        [Header("使用するエネミーを設定してください。")]
        public List<EnemyParam> enemyParams = null!;

        [Header("時間による変化を設定してください。")]
        [Header("設定されてない場合は上のデフォルト値が使用されます。")]
        public List<EnemyTimeLineParam> enemyTimeLines = null!;

    }

    [Serializable]
    public sealed class EnemySpawnParam
    {
        [Header("エネミーの生成割合 (相対的に指定可)")]
        [Min(0)]
        public float spawnRatio;

        [Header("台パンの影響を受けるイライラ度の閾値")]
        [Min(0)]
        public int daipanThreshold;

        [Header("台パンされたときに死ぬ確率 (0～1)")]
        [Range(0.0f, 1.0f)]
        [Tooltip("最終的には別途、イライラ度に応じた確率シートを作成する")]
        public float daipanProbability;
    }


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

