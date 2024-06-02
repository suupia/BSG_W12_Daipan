#nullable enable
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyPositionMonoBuilder
    {
        public EnemyPositionMonoBuilder(
            IContainerBuilder builder,
            EnemyPositionMonoData enemyPositionMonoData
        )
        {
            var data = new EnemyPositionMonoData()
            {
                GetEnemySpawnedPoints = () => enemyPositionMonoData.GetEnemySpawnedPoints(),
                GetEnemyDespawnedPoint = () => enemyPositionMonoData.GetEnemyDespawnedPoint()
            };
            builder.RegisterInstance(data);
        }
    }
}