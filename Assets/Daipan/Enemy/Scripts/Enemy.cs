#nullable enable
using Daipan.Enemy.Interfaces;

namespace Daipan.Enemy.Scripts
{
    public class Enemy : IEnemyAttack
    {
        readonly EnemyAttackDecider _enemyAttackDecider;
        public Enemy(EnemyAttackDecider enemyAttackDecider)
        {
            _enemyAttackDecider = enemyAttackDecider;
        }
        
        // IEnemyAttack
        public void AttackUpdate(PlayerMono playerMono)
        {
            _enemyAttackDecider.AttackUpdate(playerMono);
        } 
    }
}