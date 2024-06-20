#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    public class EnemyParamModifyWithTimer
    {
        readonly StreamTimer _streamTimer;
        readonly EnemyParamWarpContainer _enemyParamWarpContainer;
        readonly EnemyTimeLineParamWrapContainer _enemyTimeLineParamWrapContainer;

        [Inject]
        EnemyParamModifyWithTimer(
            StreamTimer streamTimer,
            EnemyParamWarpContainer enemyParamWarpContainer,
            EnemyTimeLineParamWrapContainer enemyTimeLineParamWrapContainer)
        {
            _streamTimer = streamTimer;
            _enemyParamWarpContainer = enemyParamWarpContainer;
            _enemyTimeLineParamWrapContainer = enemyTimeLineParamWrapContainer;
        }

        // EnemyParamDataの一部をデコレートしている

        public double GetSpeedRate(EnemyEnum enemyEnum)
        {
            return _enemyParamWarpContainer.GetEnemyParamData(enemyEnum).GetMoveSpeedPreSec() *
                   GetEnemyTimeLineParam().GetMoveSpeedRate();
        }

        public double GetSpawnIntervalSec()
        {
            return GetEnemyTimeLineParam().GetSpawnIntervalSec();
        }

        public double GetSpawnBossPercent()
        {
            return GetEnemyTimeLineParam().GetSpawnBossPercent();
        }

        EnemyTimeLineParamWarp GetEnemyTimeLineParam()
        {
            return _enemyTimeLineParamWrapContainer.GetEnemyTimeLineParamData(_streamTimer);
        }
    }
}