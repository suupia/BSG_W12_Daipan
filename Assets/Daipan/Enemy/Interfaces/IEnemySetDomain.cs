#nullable enable
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemySetDomain
    {
        public void SetDomain(
            EnemyEnum enemyEnum
            , IEnemyCluster enemyCluster
            , EnemyAttackDecider enemyAttackDecider
            , EnemyDie enemyDie
            , IEnemyOnAttacked enemyOnAttacked
            , IEnemyOnDied enemyOnDied
        );
    }
}