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
            var data = new EnemySpawnPointData()
            {
                GetEnemySpawnedPoints = () => enemyPositionMono.enemySpawnedPoints,
                GetEnemyDespawnedPoint = () => enemyPositionMono.enemyDespawnedPoint.transform.position,
            };
            builder.RegisterInstance(data);
        }
    }
}