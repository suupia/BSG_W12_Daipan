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
        [FormerlySerializedAs("EnemyType")]
        [Header("敵のレベルデザインはこちら")] [Space] 
        [Header("敵の種類")] [SerializeField]
        public EnemyEnum enemyEnum = EnemyEnum.None;

        public EnemyAttackParam enemyAttackParam = null!;
        public EnemyHpParam enemyHpParam = null!;
        public EnemyMoveParam enemyMoveParam = null!;
        public EnemySpawnParam enemySpawnParam = null!;
        
        [Header("AnimatorController")]
        public RuntimeAnimatorController animatorController = null!;

    }
    
    
    public enum EnemyEnum {
        None,
        Red,
        Blue,
        Yellow,
        [IsBoss(true)]
        RedBoss,
        [IsBoss(true)]
        BlueBoss,
        [IsBoss(true)]
        YellowBoss,
    }

    public static class AnyTypesExtensions{
        public static bool? IsBoss(this EnemyEnum self)
        {
            var fieldInfo = self.GetType().GetField(self.ToString());
            return fieldInfo?.GetCustomAttribute<IsBossAttribute>()?.IsBoss;
        }
    }

    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    class IsBossAttribute : Attribute
    {
        public bool IsBoss {get;}
        public IsBossAttribute(bool isBoss) : base() => this.IsBoss = isBoss;
    }


}
