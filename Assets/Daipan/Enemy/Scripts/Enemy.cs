#nullable enable
using Daipan.Enemy.Interfaces;

namespace Daipan.Enemy.Scripts
{
    public class Enemy : IEnemyAttack
    {
        readonly EnemyAttack _enemyAttack;
        public Enemy(EnemyAttack enemyAttack)
        {
            _enemyAttack = enemyAttack;
        }
        
        // IEnemyAttack
        public void AttackUpdate(PlayerMono playerMono)
        {
            _enemyAttack.AttackUpdate(playerMono);
        } 
    }
}