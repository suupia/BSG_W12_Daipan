#nullable enable
using System;
using System.Linq;
using System.Reflection;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    [Serializable]
    public sealed class EnemyAttackParam
    {
        [Header("敵の攻撃力")] [Min(0)] public int attackAmount = 1;

        [FormerlySerializedAs("attackDelaySec")] [Header("攻撃間隔")] [Min(0)] public float attackIntervalSec = 1;

        [Header("攻撃範囲")] [Min(0)] public float attackRange = 2;
        
        [Header("一回の攻撃で貯まるイライラゲージの量")] [Min(0)] public int increaseIrritatedValueOnAttack = 10;
    }

    [Serializable]
    public sealed class EnemyHpParam
    {
        [Header("敵の最大HP")] [Min(0)] public int maxHp = 10;
    }

    [Serializable]
    public sealed class EnemyMoveParam
    {
        [Header("移動速度 [unit/s]")] [Min(0)] public float moveSpeedPerSec;
    }

    [Serializable]
    public sealed class EnemySpawnParam
    {
        [Header("敵の生成割合 (相対的に指定可)")] [Min(0)] public double spawnRatio;

    }


    [Serializable]
    public sealed class EnemyAnimatorParam
    {
        [Header("AnimatorController")] public RuntimeAnimatorController animatorController = null!;

        public Color bodyColor = Color.white;
        public Color eyeColor = new(226f / 255f, 248f / 255f, 227f / 255f);
        public Color eyeBallColor = Color.white;
        public Color lineColor = new(111f / 255f, 87f / 255f, 107f / 255f);
    }

    [Serializable]
    public sealed class EnemyParam
    {
        [FormerlySerializedAs("EnemyType")] [Header("敵のレベルデザインはこちら")] [Space] [Header("敵の種類")] [SerializeField]
        public EnemyEnum enemyEnum = EnemyEnum.None;

        public EnemyAttackParam enemyAttackParam = null!;
        public EnemyHpParam enemyHpParam = null!;
        public EnemyMoveParam enemyMoveParam = null!;
        public EnemySpawnParam enemySpawnParam = null!;
        public EnemyAnimatorParam enemyAnimatorParam = null!;
    }
}