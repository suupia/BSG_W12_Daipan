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
    public class EnemyParamModifyWithTimer
    {
        readonly StreamTimer _streamTimer;
        readonly EnemyParamDataContainer _enemyParamDataContainer;
        readonly EnemyTimeLineParamDataContainer _enemyTimeLineParamDataContainer;

        [Inject]
        EnemyParamModifyWithTimer(
            StreamTimer streamTimer,
            EnemyParamDataContainer enemyParamDataContainer,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer)
        {
            _streamTimer = streamTimer;
            _enemyParamDataContainer = enemyParamDataContainer;
            _enemyTimeLineParamDataContainer = enemyTimeLineParamDataContainer;

        }

        // EnemyParamDataの一部をデコレートしている

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

    }
}