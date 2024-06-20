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
    public class EnemyParamDataContainer : IEnemyParamContainer
    {
        public IEnumerable<EnemyParamData> EnemyParamDatas => _enemyParamDatas;
        readonly IEnumerable<EnemyParamData> _enemyParamDatas;

        [Inject]
        public EnemyParamDataContainer(
            EnemyParamManager enemyParamManager,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer,
            StreamTimer streamTimer)
        {
            _enemyParamDatas = CreateEnemyParamData(enemyParamManager, enemyTimeLineParamDataContainer, streamTimer);
        }

        public EnemyParamData GetEnemyParamData(EnemyEnum enemyEnum)
        {
            return _enemyParamDatas.First(x => x.GetEnemyEnum() == enemyEnum);
        }

        static List<EnemyParamData> CreateEnemyParamData(EnemyParamManager enemyParamManager,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer, StreamTimer streamTimer)
        {
            var enemyParams = new List<EnemyParamData>();
            foreach (var enemyParam in enemyParamManager.enemyParams)
                enemyParams.Add(new EnemyParamData()
                {
                    GetEnemyEnum = () => enemyParam.enemyEnum,
                    GetAttackAmount = () => enemyParam.enemyAttackParam.attackAmount,
                    GetAttackDelayDec = () => enemyParam.enemyAttackParam.attackDelaySec,
                    GetAttackRange = () => enemyParam.enemyAttackParam.attackRange,
                    GetCurrentHp = () => enemyParam.enemyHpParam.hpAmount,
                    GetMoveSpeedPreSec = () =>
                        enemyParam.enemyMoveParam.moveSpeedPerSec *
                        GetEnemyTimeLineParam(enemyTimeLineParamDataContainer, streamTimer).GetMoveSpeedRate(),
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

        static EnemyTimeLineParamData GetEnemyTimeLineParam(
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer, StreamTimer streamTimer)
        {
            return enemyTimeLineParamDataContainer.GetEnemyTimeLineParamData(streamTimer);
        }
    }
}