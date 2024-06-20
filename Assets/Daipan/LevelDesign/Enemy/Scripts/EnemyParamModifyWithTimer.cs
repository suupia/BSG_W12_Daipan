#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
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
        readonly EnemyParamWarpContainer _enemyParamWarpContainer;
        readonly EnemyTimeLineParamDataContainer _enemyTimeLineParamDataContainer;

        [Inject]
        EnemyParamModifyWithTimer(
            StreamTimer streamTimer,
            EnemyParamWarpContainer enemyParamWarpContainer,
            EnemyTimeLineParamDataContainer enemyTimeLineParamDataContainer)
        {
            _streamTimer = streamTimer;
            _enemyParamWarpContainer = enemyParamWarpContainer;
            _enemyTimeLineParamDataContainer = enemyTimeLineParamDataContainer;

        }

        // EnemyParamDataの一部をデコレートしている

        public double GetSpeedRate(EnemyEnum enemyEnum)
        {
            return _enemyParamWarpContainer.GetEnemyParamData(enemyEnum).GetMoveSpeedPreSec() *
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

        EnemyTimeLineParamWarp GetEnemyTimeLineParam()
        {
            return _enemyTimeLineParamDataContainer.GetEnemyTimeLineParamData(_streamTimer); 
        }

    }
}