#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
 
    [Serializable]
    public sealed class EnemyAttackParam
    {
        [Header("敵の攻撃力")]
        [Min(0)]
        public int attackAmount;

        [Header("攻撃間隔")]
        [Min(0)]
        public float attackDelaySec;

        [Header("攻撃範囲")]
        [Min(0)]
        public float attackRange;
    }

    [Serializable]
    public sealed class EnemyHpParam
    {
        [Header("敵のHP")]
        [Min(0)]
        public int hpAmount;
    }

    [Serializable]
    public sealed class EnemyMoveParam
    {
        [Header("移動速度 [unit/s]")]
        [Min(0)]
        public float moveSpeedPerSec;
    }
    
    [Serializable]
    public sealed class EnemySpawnParam
    {
        [Header("敵の生成割合 (相対的に指定可)")]
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
    public sealed class EnemyParam
    {
        [Header("敵のレベルデザインはこちら")]
        [Space]

        [Header("敵の種類")]
        [SerializeField]
        EnemyType enemyType = EnemyType.None;

        public EnemyAttackParam enemyAttackParam = null!;
        public EnemyHpParam enemyHpParam = null!;
        public EnemyMoveParam enemyMoveParam = null!;
        public EnemySpawnParam enemySpawnParam = null!;

        [Header("スプライト")]
        public Sprite sprite = null!;
        
        [Header("AnimatorController")]
        public RuntimeAnimatorController animatorController = null!;

        public EnemyParam()
        {
            EnumEnumerationChecker.CheckEnum<EnemyType,EnemyEnum>();
        }

        public EnemyEnum GetEnemyEnum => EnemyEnum.Values.First(x => x.Name == enemyType.ToString()); 

        enum EnemyType
        {
            None,
            W,
            A,
            S,
            Boss
        }
    }

}
