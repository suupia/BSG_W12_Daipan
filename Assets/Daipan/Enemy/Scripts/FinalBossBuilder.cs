#nullable enable
using Daipan.Battle.scripts;
using Daipan.Comment.Interfaces;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossBuilder
    {
        readonly IEnemyCluster _enemyCluster;
        readonly IEnemySpawner _enemySpawner;
        readonly FinalBossOnAttacked _finalBossOnAttacked;
        readonly FinalBossDefeatTracker _finalBossDefeatTracker;

        readonly IFinalBossCurrentColor _finalBossCurrentColor;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly ICommentSpawner _commentSpawner;
        readonly IFinalBossParamData _finalBossParamData;

        readonly ComboSpawner _comboSpawner;
        readonly ComboCounter _comboCounter;

        public FinalBossBuilder(
            IEnemyCluster enemyCluster
            , IEnemySpawner enemySpawner
            , FinalBossOnAttacked finalBossOnAttacked
            , FinalBossDefeatTracker finalBossDefeatTracker
            , IFinalBossCurrentColor finalBossCurrentColor
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , ICommentSpawner commentSpawner
            , IFinalBossParamData finalBossParamData
            , ComboSpawner comboSpawner
            , ComboCounter comboCounter
        )
        {
            _enemyCluster = enemyCluster;
            _enemySpawner = enemySpawner;
            _finalBossOnAttacked = finalBossOnAttacked;
            _finalBossDefeatTracker = finalBossDefeatTracker;
            _finalBossCurrentColor = finalBossCurrentColor;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _commentSpawner = commentSpawner;
            _finalBossParamData = finalBossParamData;
            _comboSpawner = comboSpawner;
            _comboCounter = comboCounter;
        }

        public IEnemyMono Build(IEnemyMono finalBossMono, IFinalBossSetDomain finalBossSetDomain, EnemyEnum enemyEnum)
        {
            finalBossSetDomain.SetDomain(
                enemyEnum
                , _enemyCluster
                , new FinalBossActionDecider(_enemySpawner)
                , new FinalBossDie(finalBossMono)
                , new EnemyBuilder.EnemyOnAttackedWithComboSpawner(_finalBossOnAttacked, finalBossMono, _comboSpawner, _comboCounter)
            );

            finalBossMono.OnAttackedEvent += (sender, args) =>
            {

                if (FinalBossOnAttacked.IsSameColor(_finalBossCurrentColor.CurrentColor, args.PlayerEnum())) return;

                var spawnPercent = _playerAntiCommentParamData.GetFinalBossAntiCommentPercentOnMissAttacks();

                if (spawnPercent / 100f > UnityEngine.Random.value)
                {
                    _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                }
            };

            finalBossMono.OnDiedEvent += (sender, args) =>
            {
                _finalBossDefeatTracker.SetFinalBossDefeated();

                SpawnComment(args, _commentSpawner, _finalBossParamData.GetCommentCount());
            };


            return finalBossMono;
        }

        static void SpawnComment(DiedEventArgs args, ICommentSpawner commentSpawner, int count)
        {
            for (var i = 0; i < count; i++) commentSpawner.SpawnCommentByType(CommentEnum.Normal);
        }
    }
}