#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemyEnumSelector : IEnemyEnumSelector
    {
        readonly EnemyParamsManager _enemyParamsManager;
        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;

        public EnemyEnumSelector(
            EnemyParamsManager enemyParamsManager
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , IEnemyWaveParamContainer enemyWaveParamContainer
        )
        {
            _enemyParamsManager = enemyParamsManager;
            _enemyWaveParamContainer = enemyWaveParamContainer;
        }

        public EnemyEnum SelectEnemyEnum()
        {
            return
                IsSpawnBoss( _enemyWaveParamContainer)
                    ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsBoss() == true)
                    : IsSpawnSpecial(_enemyWaveParamContainer)
                        ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsSpecial() == true)
                        : IsSpawnTotem(_enemyWaveParamContainer)
                            ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsTotem() == true)
                            : DecideRandomEnemyType(_enemyParamsManager,
                                x => x.IsBoss() != true && x.IsSpecial() != true && x.IsTotem() != true);
        }

        static bool IsSpawnBoss(
             IEnemyWaveParamContainer enemyWaveParamContainer
        )
        {
            // Bossが出現する条件
            if (Random.value < enemyWaveParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent() / 100.0)
                return true;

            return false;
        }

        static bool IsSpawnSpecial(IEnemyWaveParamContainer enemyWaveParamContainer)
        {
            // Specialが出現する条件
            if (Random.value < enemyWaveParamContainer.GetEnemyTimeLineParamData().GetSpawnSpecialPercent() /
                100.0) return true;
            return false;
        }

        static bool IsSpawnTotem(IEnemyWaveParamContainer enemyWaveParamContainer)
        {
            // Totemが出現する条件
            if (Random.value < enemyWaveParamContainer.GetEnemyTimeLineParamData().GetSpawnTotemPercent() / 100.0)
                return true;
            return false;
        }

        static EnemyEnum DecideRandomEnemyType(
            EnemyParamsManager enemyParamsManager
            , Func<EnemyEnum, bool> targetEnemyEnum
        )
        {
            List<(EnemyEnum EnemyEnum, double Ratio)> table = enemyParamsManager.enemyParams
                .Where(x => targetEnemyEnum(x.enemyEnum))
                .Select(x => (x.enemyEnum, x.enemySpawnParam.spawnRatio))
                .ToList();
            var randomIndex = Randoms.RandomByRatios(table.Select(x => x.Ratio).ToList(), Random.value);
            return table[randomIndex].EnemyEnum;
        }
    }
}