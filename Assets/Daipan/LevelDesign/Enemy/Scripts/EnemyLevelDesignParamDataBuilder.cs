#nullable enable
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyLevelDesignParamDataBuilder
    {
        public EnemyLevelDesignParamDataBuilder(
            IContainerBuilder builder,
            EnemyLevelDesignParam levelDesignParam
        )
        {
            var data = new EnemyLevelDesignParamData()
            {
                GetSpawnBossAmount = () => levelDesignParam.spawnBossAmount,
                GetIncreaseIrritatedValueByBoss = () => levelDesignParam.increaseIrritatedValueByBoss,
                increaseViewerOnEnemyKill = () => levelDesignParam.increaseViewerOnEnemyKill,
                GetCurrentKillAmount = () => levelDesignParam.currentKillAmount,
            };
            builder.RegisterInstance(data);
        }
    }
}