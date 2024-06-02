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
                spawnBossAmount = () => levelDesignParam.spawnBossAmount,
                increaseIrritatedValueByBoss = () => levelDesignParam.increaseIrritatedValueByBoss,
                currentKillAmount = () => levelDesignParam.currentKillAmount,
            };
            builder.RegisterInstance(data);
        }
    }
}