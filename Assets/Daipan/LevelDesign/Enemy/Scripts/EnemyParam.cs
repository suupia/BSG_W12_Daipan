#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
 
    [Serializable]
    public sealed class EnemyAttackParam
    {
        [Header("エネミーの攻撃力")]
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
        [Header("エネミーのHP")]
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
    public sealed class EnemyParam
    {
        [Header("Enemyのレベルデザインはこちら")]
        [Space(30)]

        [Header("Enemyの種類")]
        [SerializeField]
        EnemyType enemyType = EnemyType.None;

        public EnemySpawnParam enemySpawnParam = null!;
        public EnemyAttackParam enemyAttackParam = null!;
        public EnemyHpParam enemyHpParam = null!;
        public EnemyMoveParam enemyMoveParam = null!;

        [Header("スプライト")]
        public Sprite sprite = null!;


        public EnemyEnum GetEnemyEnum
        {
            get
            {
                EnemyEnumChecker.CheckEnum();
                return EnemyEnum.Values.First(x => x.Name == enemyType.ToString());
            }
        }

        static class EnemyEnumChecker
        {
            static bool _isCheckedEnum;

            public static void CheckEnum()
            {
                if (_isCheckedEnum) return;
                foreach (var type in Enum.GetValues(typeof(EnemyType)).Cast<EnemyType>())
                {
                    var enemy = EnemyEnum.Values.FirstOrDefault(x => x.Name == type.ToString());
                    if (enemy.Equals(default(EnemyEnum)))
                        Debug.LogWarning($"EnemyEnum with name {type.ToString()} not found.");
                }

                _isCheckedEnum = true;
            }
        }

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
