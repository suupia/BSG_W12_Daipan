#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamWarpBuilder
    {
        public EnemyParamWarpBuilder(
            IContainerBuilder builder,
            EnemyParamManager enemyParamManager
            )
        {
            var enemyParams = new List<EnemyParamWarp>();
            foreach (var enemyParam in enemyParamManager.enemyParams)
            {
                enemyParams.Add(new EnemyParamWarp()
                {
                    GetEnemyEnum = () => enemyParam.enemyEnum,
                    GetAttackAmount = () => enemyParam.enemyAttackParam.attackAmount,
                    GetAttackDelayDec = () => enemyParam.enemyAttackParam.attackDelaySec,
                    GetAttackRange = () => enemyParam.enemyAttackParam.attackRange,
                    GetCurrentHp = () => enemyParam.enemyHpParam.hpAmount,
                    GetMoveSpeedPreSec = () => enemyParam.enemyMoveParam.moveSpeedPerSec,
                    GetSpawnRatio = () => enemyParam.enemySpawnParam.spawnRatio,
                    GetIrritationAfterKill = () => enemyParam.enemyRewardParam.irritationAfterKill,
                    
                    // Animator
                    GetBodyColor = () => enemyParam.enemyAnimatorParam.bodyColor,
                    GetEyeColor = () => enemyParam.enemyAnimatorParam.eyeColor,
                    GetEyeBallColor = () => enemyParam.enemyAnimatorParam.eyeBallColor,
                    GetLineColor = () => enemyParam.enemyAnimatorParam.lineColor,
                    
                }); 
            }
            var enemyParamContainer = new EnemyParamWarpContainer(enemyParams);
            builder.RegisterInstance(enemyParamContainer);
        }
    } 
}

