#nullable enable
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyPositionMonoBuilder
    {
        public EnemyPositionMonoBuilder(
            IContainerBuilder builder,
            EnemyPositionMono enemyPositionMono
        )
        {
            var data = new EnemyPositionMonoData()
            {
                GetEnemySpawnedPoints = () => enemyPositionMono.enemySpawnedPoints,
                GetEnemyDespawnedPoint = () => enemyPositionMono.enemyDespawnedPoint,
            };
            builder.RegisterInstance(data);
        }
    }
}