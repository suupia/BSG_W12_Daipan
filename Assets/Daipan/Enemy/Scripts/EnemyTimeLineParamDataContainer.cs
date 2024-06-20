#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.Scripts
{
    public class EnemyTimeLineParamDataContainer : IEnemyTimeLineParamContainer
    {
        readonly IList<EnemyTimeLineParamData> _enemyTimeLineParamDatas;

        [Inject]
        public EnemyTimeLineParamDataContainer(EnemyParamManager enemyParamManager)
        {
            _enemyTimeLineParamDatas = CreateEnemyTimeLineParamData(enemyParamManager);
        }

        public EnemyTimeLineParamDataContainer(
            IList<EnemyTimeLineParamData> enemyTimeLineParamDatas
        )
        {
            _enemyTimeLineParamDatas = enemyTimeLineParamDatas;
        }

        public EnemyTimeLineParamData GetEnemyTimeLineParamData(StreamTimer streamTimer)
        {
            return _enemyTimeLineParamDatas
                .Where(e => e.GetStartTime() <= streamTimer.CurrentTime)
                .OrderByDescending(e => e.GetStartTime()).First();
        }

        static List<EnemyTimeLineParamData> CreateEnemyTimeLineParamData(EnemyParamManager enemyParamManager)
        {
            // [Precondition]
            if (enemyParamManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParams.Count is 0");
                enemyParamManager.enemyTimeLineParams.Add(new EnemyTimeLineParam());
            }


            var enemyTimeLineParams = new List<EnemyTimeLineParamData>();
            foreach (var enemyTimeLineParam in enemyParamManager.enemyTimeLineParams)
                enemyTimeLineParams.Add(new EnemyTimeLineParamData()
                {
                    GetStartTime = () => enemyTimeLineParam.startTime,
                    GetSpawnIntervalSec = () => enemyTimeLineParam.spawnIntervalSec,
                    GetMoveSpeedRate = () => enemyTimeLineParam.moveSpeedRate,
                    GetSpawnBossPercent = () => enemyTimeLineParam.spawnBossPercent
                });
            return enemyTimeLineParams;
        }
    }
}