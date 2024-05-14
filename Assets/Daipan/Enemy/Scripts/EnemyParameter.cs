#nullable enable
using System;
using UnityEngine;


namespace Daipan.Enemy.Scripts
{
    [Serializable]
    public sealed class EnemyAttackParameter
    {
        public int AttackAmount;
    }

    [Serializable]
    public sealed class EnemyHPParameter
    {
        public int HPAmount;
    }

    [Serializable]
    public sealed class EnemyMovementParameter
    {
        public float MovementAmount;
    }

    [Serializable]
    public sealed class EnemyAttackCoolTime
    {
        public float MaxCoolTime;
        public float MinCoolTime;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EnemyParameter", order = 1)]
    public sealed class EnemyParameter : ScriptableObject
    {
        public EnemyType enemyType = EnemyType.None;
        public EnemyAttackParameter attackParameter = null!;
        public EnemyHPParameter hpParameter = null!;
        public EnemyMovementParameter movementParameter = null!;
        public EnemyAttackCoolTime attackCoolTime = null!;
    }
}