#nullable enable
using VContainer;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    public sealed class EnemyLevelDesignParamDataBuilder
    {
        public EnemyLevelDesignParamDataBuilder(
            IContainerBuilder builder,
            EnemyLevelDesignParam levelDesignParam
        )
        {
            var data = new EnemyLevelDesignParamData()
            {
                GetIncreaseViewerOnEnemyKill = () => levelDesignParam.increaseViewerOnEnemyKill,
                GetSpawnBossAmount = () => levelDesignParam.spawnBossAmount,
                GetCurrentKillAmount = () => levelDesignParam.currentKillAmount,
                SetCurrentKillAmount = value => levelDesignParam.currentKillAmount = value
            };
            builder.RegisterInstance(data);
        }
    }
}