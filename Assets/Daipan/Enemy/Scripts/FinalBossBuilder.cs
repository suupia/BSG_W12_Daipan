#nullable enable
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossBuilder
    {
        readonly EnemyCluster _enemyCluster;
        readonly EnemySpawner _enemySpawner;
        readonly FinalBossOnAttacked _finalBossOnAttacked;
        readonly FinalBossDefeatTracker _finalBossDefeatTracker;
        
        readonly FinalBossColorChanger _finalBossColorChanger;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly CommentSpawner _commentSpawner;
        public FinalBossBuilder(
            EnemyCluster enemyCluster
            , EnemySpawner enemySpawner
            , FinalBossOnAttacked finalBossOnAttacked
            , FinalBossDefeatTracker finalBossDefeatTracker
            , FinalBossColorChanger finalBossColorChanger
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , CommentSpawner commentSpawner
        )
        {
            _enemyCluster = enemyCluster;
            _enemySpawner = enemySpawner;
            _finalBossOnAttacked = finalBossOnAttacked;
            _finalBossDefeatTracker = finalBossDefeatTracker;
            _finalBossColorChanger = finalBossColorChanger;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _commentSpawner = commentSpawner;
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
            
            finalBossMono.OnAttackedEvent += (sender, args) =>
            {

                if (FinalBossOnAttacked.IsSameColor(_finalBossColorChanger.CurrentColor, args.PlayerEnum())) return;

                var spawnPercent = _playerAntiCommentParamData.GetFinalBossAntiCommentPercentOnMissAttacks();
            
                if (spawnPercent / 100f > UnityEngine.Random.value)
                {
                    _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                }
            };
            
            finalBossMono.OnDiedEvent += (sender, args) =>
            {
                _finalBossDefeatTracker.SetFinalBossDefeated();
            };

            return finalBossMono;
        }
    }
}