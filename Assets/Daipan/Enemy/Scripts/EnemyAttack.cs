#nullable enable
using System.Data.Common;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttack
    {
        public EnemyAttackParameter _enemyAttackParameter;
        public void Attack()
        {
            Debug.Log($"Temp Attack  {_enemyAttackParameter.AttackAmount}");
        }
    }
}