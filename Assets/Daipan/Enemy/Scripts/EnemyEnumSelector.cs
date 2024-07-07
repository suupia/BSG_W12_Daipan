#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemyEnumSelector
    {
        readonly EnemyParamsManager _enemyParamsManager;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly IEnemyTimeLineParamContainer _enemyTimeLineParamContainer;

        public EnemyEnumSelector(
            EnemyParamsManager enemyParamsManager
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , IEnemyTimeLineParamContainer enemyTimeLineParamContainer
        )
        {
            _enemyParamsManager = enemyParamsManager;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyTimeLineParamContainer = enemyTimeLineParamContainer;
        }

        public EnemyEnum SelectEnemyEnum()
        {
            return
                IsSpawnBoss(_enemyLevelDesignParamData, _enemyTimeLineParamContainer)
                    ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsBoss() == true)
                    : IsSpawnSpecial(_enemyTimeLineParamContainer)
                        ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsSpecial() == true)
                        : IsSpawnTotem(_enemyTimeLineParamContainer)
                            ? DecideRandomEnemyType(_enemyParamsManager, x => x.IsTotem() == true)
                            : DecideRandomEnemyType(_enemyParamsManager,
                                x => x.IsBoss() != true && x.IsSpecial() != true && x.IsTotem() != true);
        }

        static bool IsSpawnBoss(
            EnemyLevelDesignParamData enemyLevelDesignParamData
            , IEnemyTimeLineParamContainer enemyTimeLineParamContainer
        )
        {
            // Bossが出現する条件1
            if (enemyLevelDesignParamData.GetCurrentKillAmount() >= enemyLevelDesignParamData.GetSpawnBossAmount())
            {
                enemyLevelDesignParamData.SetCurrentKillAmount(0);
                return true;
            }

            // Bossが出現する条件2
            if (Random.value < enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent() / 100.0)
                return true;

            return false;
        }

        static bool IsSpawnSpecial(IEnemyTimeLineParamContainer enemyTimeLineParamContainer)
        {
            // Specialが出現する条件
            if (Random.value < enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnSpecialPercent() /
                100.0) return true;
            return false;
        }

        static bool IsSpawnTotem(IEnemyTimeLineParamContainer enemyTimeLineParamContainer)
        {
            // Totemが出現する条件
            if (Random.value < enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnTotemPercent() / 100.0)
                return true;
            return false;
        }

        static EnemyEnum DecideRandomEnemyType(EnemyParamsManager enemyParamsManager,
            Func<EnemyEnum, bool> targetEnemyEnum)
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