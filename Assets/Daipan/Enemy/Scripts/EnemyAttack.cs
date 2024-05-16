#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttack
    {
        public EnemyAttackParameter enemyAttackParameter;

        public void Attack()
        {
            Debug.Log($"Temp Attack  {enemyAttackParameter.attackAmount}");
        }
    }
}