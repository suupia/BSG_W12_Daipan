#nullable enable
using System.Collections.Generic;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyTimeLineParamDataBuilder
    {
        public EnemyTimeLineParamDataBuilder(
            IContainerBuilder builder,
            EnemyParamManager enemyParamManager
        )
        {
            var enemyTimeLineParams = new List<EnemyTimeLineParamData>();
            foreach(var enemyTimeLineParam in enemyParamManager.enemyTimeLineParams)
            {
                enemyTimeLineParams.Add(new EnemyTimeLineParamData()
                {
                    GetStartTime = () => enemyTimeLineParam.startTime,
                    GetSpawnDelaySec = () => enemyTimeLineParam.spawnDelaySec,
                    GetMoveSpeedRate = () => enemyTimeLineParam.moveSpeedRate,
                    GetSpawnBossPercent = () => enemyTimeLineParam.spawnBossPercent
                });
            }
            var enemyTimeLineParamContainer = new EnemyTimeLineParamDataContainer(enemyTimeLineParams);
            builder.RegisterInstance(enemyTimeLineParamContainer);
        }
    
    } 
}

