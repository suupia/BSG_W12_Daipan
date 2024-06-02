#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamsConfig
    {
        readonly EnemyPositionMonoData _enemyPositionMonoData;
        readonly StreamTimer _streamTimer;
        readonly EnemyParamDataContainer _enemyParamDataContainer;
        readonly EnemyTimeLineParamDataContainer _enemyTimeLineParamDataContainer;

        [Inject]
        EnemyParamsConfig(
            EnemyPositionMonoData enemyPositionMonoData,
            StreamTimer streamTimer,
            EnemyParamDataContainer enemyParamDataContainer,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer)
        {
            _enemyPositionMonoData = enemyPositionMonoData;
            _streamTimer = streamTimer;
            _enemyParamDataContainer = enemyParamDataContainer;
            _enemyTimeLineParamDataContainer = enemyTimeLineParamDataContainer;

        }


        // EnemyTimeLineParamDataの一部をデコレートしている

        public double GetSpeedRate(EnemyEnum enemyEnum)
        {
            return _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetMoveSpeedPreSec() *
                   GetEnemyTimeLineParam().GetMoveSpeedRate();
        }

        public double GetSpawnDelaySec()
        {
            return GetEnemyTimeLineParam().GetSpawnDelaySec();
        }
        public double GetSpawnBossPercent()
        {
            return GetEnemyTimeLineParam().GetSpawnBossPercent();
        }

        EnemyTimeLineParamData GetEnemyTimeLineParam()
        {
            return _enemyTimeLineParamDataContainer.GetEnemyTimeLineParamData(_streamTimer); 
        }



        #region Position

        public Vector3 GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<float> ratio = new();

            foreach (var point in _enemyPositionMonoData.GetEnemySpawnedPoints())
            {
                position.Add(point.transform.position);
                ratio.Add(point.ratio);
            }

            return position[Randoms.RandomByRatio(ratio)];
        }

        #endregion
    }
}