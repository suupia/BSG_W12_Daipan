#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamDataBuilder
    {
        public EnemyParamDataBuilder(
            IContainerBuilder builder,
            EnemyParamManager enemyParamManager
            )
        {
            var enemyParams = new List<EnemyParamData>();
            foreach (var enemyParam in enemyParamManager.enemyParams)
            {
                enemyParams.Add(new EnemyParamData()
                {
                    GetAnimator = () => enemyParam.animatorController,
                    EnemyEnum = () => enemyParam.GetEnemyEnum,
                    GetAttackAmount = () => enemyParam.enemyAttackParam.attackAmount,
                    GetAttackDelayDec = () => enemyParam.enemyAttackParam.attackDelaySec,
                    GetAttackRange = () => enemyParam.enemyAttackParam.attackRange,
                    GetCurrentHp = () => enemyParam.enemyHpParam.hpAmount,
                    GetMoveSpeedPreSec = () => enemyParam.enemyMoveParam.moveSpeedPerSec,
                    GetSpawnRatio = () => enemyParam.enemySpawnParam.spawnRatio,
                }); 
            }
            var enemyParamContainer = new EnemyParamDataContainer(enemyParams);
            builder.RegisterInstance(enemyParamContainer);
        }
    } 
}

