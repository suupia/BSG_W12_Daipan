#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossBuilder
    {
        readonly EnemyCluster _enemyCluster;
        readonly EnemySpawner _enemySpawner;
        readonly FinalBossOnAttacked _finalBossOnAttacked;

        public FinalBossBuilder(
            EnemyCluster enemyCluster
            , EnemySpawner enemySpawner
            , FinalBossOnAttacked finalBossOnAttacked
        )
        {
            _enemyCluster = enemyCluster;
            _enemySpawner = enemySpawner;
            _finalBossOnAttacked = finalBossOnAttacked;
        }

        public FinalBossMono Build(FinalBossMono finalBossMono, EnemyEnum enemyEnum)
        {
            finalBossMono.SetDomain(
                enemyEnum
                , _enemyCluster
                , new FinalBossActionDecider(_enemySpawner)
                , new FinalBossDie(finalBossMono)
                , _finalBossOnAttacked
            );


            return finalBossMono;
        }
    }
}