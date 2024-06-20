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
    public class EnemyTimeLineParamContainer : IEnemyTimeLineParamContainer
    {
        readonly IList<EnemyTimeLineParamData> _enemyTimeLineParamDatas;
        readonly StreamTimer _streamTimer;
        [Inject]
        public EnemyTimeLineParamContainer(
            EnemyParamManager enemyParamManager,
            StreamTimer streamTimer)
        {
            _enemyTimeLineParamDatas = CreateEnemyTimeLineParamData(enemyParamManager);
            _streamTimer = streamTimer;
        }

        public EnemyTimeLineParamData GetEnemyTimeLineParamData()
        {
            return GetEnemyTimeLineParamData(_streamTimer, _enemyTimeLineParamDatas).data; 
        }
        
        public int GetEnemyTimeLineParamDataIndex()
        {
            return GetEnemyTimeLineParamData(_streamTimer, _enemyTimeLineParamDatas).index;
        }
        
        static (EnemyTimeLineParamData data, int index) GetEnemyTimeLineParamData(StreamTimer streamTimer, IList<EnemyTimeLineParamData> enemyTimeLineParamDatas)
        {
            return enemyTimeLineParamDatas
                .Select((e, i) =>  (e, i))
                .Where(e => e.e.GetStartTime() <= streamTimer.CurrentTime)
                .OrderByDescending(e => e.e.GetStartTime()).First();
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