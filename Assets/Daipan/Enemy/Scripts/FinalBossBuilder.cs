#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossBuilder
    {
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly EnemyOnAttackedBuilder _enemyOnAttackedBuilder;
        public FinalBossBuilder(
            CommentSpawner commentSpawner
            , ViewerNumber viewerNumber
            , EnemyCluster enemyCluster
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , EnemyOnAttackedBuilder enemyOnAttackedBuilder
        )
        {
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyOnAttackedBuilder = enemyOnAttackedBuilder;
        }
        public FinalBossMono Build(FinalBossMono finalBossMono, EnemyEnum enemyEnum)
        {
            finalBossMono.SetDomain(
                enemyEnum
                ,_enemyCluster
                ,new EnemyAttackDecider()
                ,new EnemyDie(finalBossMono)
                , _enemyOnAttackedBuilder.SwitchEnemyOnAttacked(enemyEnum)
                );
            
            
            return finalBossMono;
        }
    } 
}

