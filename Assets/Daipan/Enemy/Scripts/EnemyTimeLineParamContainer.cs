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
    public sealed class EnemyTimeLineParamContainer : IEnemyTimeLineParamContainer
    {
        readonly IList<EnemyTimeLineParamData> _enemyTimeLineParamDatas;
        readonly StreamTimer _streamTimer;
        [Inject]
        public EnemyTimeLineParamContainer(
            EnemyParamsManager enemyParamsManager,
            StreamTimer streamTimer)
        {
            _enemyTimeLineParamDatas = CreateEnemyTimeLineParamData(enemyParamsManager);
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

        static List<EnemyTimeLineParamData> CreateEnemyTimeLineParamData(EnemyParamsManager enemyParamsManager)
        {
            // [Precondition]
            if (enemyParamsManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParams.Count is 0");
                enemyParamsManager.enemyTimeLineParams.Add(new EnemyTimeLineParam());
            }


            var enemyTimeLineParams = new List<EnemyTimeLineParamData>();
            foreach (var enemyTimeLineParam in enemyParamsManager.enemyTimeLineParams)
                enemyTimeLineParams.Add(new EnemyTimeLineParamData(enemyTimeLineParam));
            return enemyTimeLineParams;
        }
    }
}