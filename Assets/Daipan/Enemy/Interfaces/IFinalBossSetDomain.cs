#nullable enable
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces;

public interface IFinalBossSetDomain
{
    public void SetDomain(
        EnemyEnum enemyEnum
        , EnemyCluster enemyCluster
        , FinalBossActionDecider finalBossActionDecider
        , FinalBossDie enemyDie
        , IEnemyOnAttacked enemyOnAttacked
    );
}