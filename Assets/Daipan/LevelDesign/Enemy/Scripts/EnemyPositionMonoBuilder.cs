#nullable enable
using System.Linq;
using Daipan.LevelDesign.Battle.Scripts;
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
                GetEnemySpawnedPointXs = () => enemyPositionMono.enemySpawnedPoints
                    .Select(x => x.enemySpawnTransformX.position).ToList(),
                GetEnemySpawnedPointYs = () => enemyPositionMono.enemySpawnedPoints
                    .Select(x => x.enemySpawnTransformY.position).ToList(),
                GetEnemySpawnedEnemyEnums = enemyPositionMono.enemySpawnedPoints
                    .Select(x => x.enemyEnum).ToList,
                GetEnemySpawnRatios = () => enemyPositionMono.enemySpawnedPoints
                    .Select(x => (double)x.enemySpawnRatio).ToList(),
                GetEnemyDespawnedPoint = () => enemyPositionMono.enemyDespawnedPoint.transform.position,
            };
            builder.RegisterInstance(data);
        }
    }
}