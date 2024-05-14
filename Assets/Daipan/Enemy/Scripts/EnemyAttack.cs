#nullable enable
using System.Data.Common;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        public EnemyAttackParameter enemyAttackParameter;
        public void Attack()
        {
            Debug.Log($"Temp Attack  {enemyAttackParameter.attackAmount}");
        }
    }
}