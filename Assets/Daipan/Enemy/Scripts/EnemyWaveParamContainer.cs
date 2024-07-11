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
    public sealed class EnemyWaveParamContainer : IEnemyWaveParamContainer
    {
        readonly IList<EnemyWaveParamData> _enemyWaveParamDatas;
        readonly StreamTimer _streamTimer;
        [Inject]
        public EnemyWaveParamContainer(
            EnemyParamsManager enemyParamsManager,
            StreamTimer streamTimer)
        {
            _enemyWaveParamDatas = CreateEnemyTimeLineParamData(enemyParamsManager);
            _streamTimer = streamTimer;
        }

        public EnemyWaveParamData GetEnemyTimeLineParamData()
        {
            return GetEnemyTimeLineParamData(_streamTimer, _enemyWaveParamDatas).data; 
        }
        
        public int GetEnemyTimeLineParamDataIndex()
        {
            return GetEnemyTimeLineParamData(_streamTimer, _enemyWaveParamDatas).index;
        }
        
        static (EnemyWaveParamData data, int index) GetEnemyTimeLineParamData(StreamTimer streamTimer, IList<EnemyWaveParamData> enemyTimeLineParamDatas)
        {
            return enemyTimeLineParamDatas
                .Select((e, i) =>  (e, i))
                .Where(e => e.e.GetStartTime() <= streamTimer.CurrentTime)
                .OrderByDescending(e => e.e.GetStartTime()).First();
        } 

        static List<EnemyWaveParamData> CreateEnemyTimeLineParamData(EnemyParamsManager enemyParamsManager)
        {
            // [Precondition]
            if (enemyParamsManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParams.Count is 0");
                enemyParamsManager.enemyTimeLineParams.Add(new EnemyWaveParam());
            }


            var enemyTimeLineParams = new List<EnemyWaveParamData>();
            foreach (var enemyTimeLineParam in enemyParamsManager.enemyTimeLineParams)
                enemyTimeLineParams.Add(new EnemyWaveParamData(enemyTimeLineParam));
            return enemyTimeLineParams;
        }
    }
}