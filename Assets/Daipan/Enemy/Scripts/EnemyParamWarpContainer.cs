#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.Enemy.Scripts
{
    public class EnemyParamWarpContainer : IEnemyParamContainer
    {
        public IEnumerable<EnemyParamWarp> EnemyParamWarps => _enemyParamWarps;
        readonly IEnumerable<EnemyParamWarp> _enemyParamWarps;

        [Inject]
        public EnemyParamWarpContainer(
            EnemyParamManager enemyParamManager,
            EnemyTimeLineParamWrapContainer enemyTimeLineParamWrapContainer,
            StreamTimer streamTimer)
        {
            _enemyParamWarps = CreateEnemyParamWarp(enemyParamManager, enemyTimeLineParamWrapContainer, streamTimer);
        }

        public EnemyParamWarp GetEnemyParamData(EnemyEnum enemyEnum)
        {
            return _enemyParamWarps.First(x => x.GetEnemyEnum() == enemyEnum);
        }

        static List<EnemyParamWarp> CreateEnemyParamWarp(EnemyParamManager enemyParamManager,
            EnemyTimeLineParamWrapContainer enemyTimeLineParamWrapContainer, StreamTimer streamTimer)
        {
            var enemyParams = new List<EnemyParamWarp>();
            foreach (var enemyParam in enemyParamManager.enemyParams)
                enemyParams.Add(new EnemyParamWarp()
                {
                    GetEnemyEnum = () => enemyParam.enemyEnum,
                    GetAttackAmount = () => enemyParam.enemyAttackParam.attackAmount,
                    GetAttackDelayDec = () => enemyParam.enemyAttackParam.attackDelaySec,
                    GetAttackRange = () => enemyParam.enemyAttackParam.attackRange,
                    GetCurrentHp = () => enemyParam.enemyHpParam.hpAmount,
                    GetMoveSpeedPreSec = () =>
                        enemyParam.enemyMoveParam.moveSpeedPerSec *
                        GetEnemyTimeLineParam(enemyTimeLineParamWrapContainer, streamTimer).GetMoveSpeedRate(),
                    GetSpawnRatio = () => enemyParam.enemySpawnParam.spawnRatio,
                    GetIrritationAfterKill = () => enemyParam.enemyRewardParam.irritationAfterKill,
                    // Animator
                    GetBodyColor = () => enemyParam.enemyAnimatorParam.bodyColor,
                    GetEyeColor = () => enemyParam.enemyAnimatorParam.eyeColor,
                    GetEyeBallColor = () => enemyParam.enemyAnimatorParam.eyeBallColor,
                    GetLineColor = () => enemyParam.enemyAnimatorParam.lineColor
                });
            return enemyParams;
        }

        static EnemyTimeLineParamWarp GetEnemyTimeLineParam(
            EnemyTimeLineParamWrapContainer enemyTimeLineParamWrapContainer, StreamTimer streamTimer)
        {
            return enemyTimeLineParamWrapContainer.GetEnemyTimeLineParamData(streamTimer);
        }
    }
}