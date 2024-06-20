#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyTimeLineParamDataContainer
    {
        readonly IList<EnemyTimeLineParamWarp> _enemyTimeLineParamDatas;

        public EnemyTimeLineParamDataContainer(EnemyParamManager enemyParamManager)
        {
            _enemyTimeLineParamDatas = CreateEnemyTimeLineParamWarp(enemyParamManager);
        }
        
        public EnemyTimeLineParamDataContainer(
            IList<EnemyTimeLineParamWarp> enemyTimeLineParamDatas
            )
        {
            _enemyTimeLineParamDatas = enemyTimeLineParamDatas;
          
        }

        public EnemyTimeLineParamWarp GetEnemyTimeLineParamData(StreamTimer streamTimer)
        {
            return _enemyTimeLineParamDatas
                .Where(e => e.GetStartTime() <= streamTimer.CurrentTime)
                .OrderByDescending(e => e.GetStartTime()).First();
        }
        static List<EnemyTimeLineParamWarp> CreateEnemyTimeLineParamWarp(EnemyParamManager enemyParamManager)
        {
            // [Precondition]
            if(enemyParamManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParams.Count is 0");
                enemyParamManager.enemyTimeLineParams.Add(new EnemyTimeLineParam());
            }
            
            
            var enemyTimeLineParams = new List<EnemyTimeLineParamWarp>();
            foreach(var enemyTimeLineParam in enemyParamManager.enemyTimeLineParams)
            {
                enemyTimeLineParams.Add(new EnemyTimeLineParamWarp()
                {
                    GetStartTime = () => enemyTimeLineParam.startTime,
                    GetSpawnIntervalSec = () => enemyTimeLineParam.spawnIntervalSec,
                    GetMoveSpeedRate = () => enemyTimeLineParam.moveSpeedRate,
                    GetSpawnBossPercent = () => enemyTimeLineParam.spawnBossPercent
                });
            }
            return enemyTimeLineParams;
        }

    }
}