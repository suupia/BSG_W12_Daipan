#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamWarpContainer
    {
        public IEnumerable<EnemyParamWarp> EnemyParamWarps => _enemyParamWarps;
        readonly IEnumerable<EnemyParamWarp> _enemyParamWarps;

        public EnemyParamWarpContainer(
            EnemyParamManager enemyParamManager,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer,
            StreamTimer streamTimer)
        {
            _enemyParamWarps = CreateEnemyParamWarp(enemyParamManager, enemyTimeLineParamDataContainer, streamTimer);
        }
        
        public EnemyParamWarpContainer(IEnumerable<EnemyParamWarp> enemyParamWarps)
        {
            _enemyParamWarps = enemyParamWarps;
        }
        public EnemyParamWarp GetEnemyParamData(EnemyEnum enemyEnum)
        {
            return _enemyParamWarps.First(x => x.GetEnemyEnum() == enemyEnum);
        }
                
        static List<EnemyParamWarp> CreateEnemyParamWarp(EnemyParamManager enemyParamManager, EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer, StreamTimer streamTimer)
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
                    GetMoveSpeedPreSec = () => enemyParam.enemyMoveParam.moveSpeedPerSec * GetEnemyTimeLineParam(enemyTimeLineParamDataContainer, streamTimer).GetMoveSpeedRate(), 
                    GetSpawnRatio = () => enemyParam.enemySpawnParam.spawnRatio,
                    GetIrritationAfterKill = () => enemyParam.enemyRewardParam.irritationAfterKill,
                    
                    // Animator
                    GetBodyColor = () => enemyParam.enemyAnimatorParam.bodyColor,
                    GetEyeColor = () => enemyParam.enemyAnimatorParam.eyeColor,
                    GetEyeBallColor = () => enemyParam.enemyAnimatorParam.eyeBallColor,
                    GetLineColor = () => enemyParam.enemyAnimatorParam.lineColor,
                    
                }); 
            }
            return enemyParams;
        }
        
        static EnemyTimeLineParamWarp GetEnemyTimeLineParam(EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer, StreamTimer streamTimer)
        {
            return enemyTimeLineParamDataContainer.GetEnemyTimeLineParamData(streamTimer); 
        }

    } 
}

