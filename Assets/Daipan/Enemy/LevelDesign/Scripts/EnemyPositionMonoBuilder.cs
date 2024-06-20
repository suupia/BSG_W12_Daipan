#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.LevelDesign.Battle.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    public class EnemyPositionMonoBuilder
    {
        public EnemyPositionMonoBuilder(
            IContainerBuilder builder,
            EnemyPositionMono enemyPositionMono,
            WaveState waveState
        )
        {
            var data = new EnemySpawnPointData()
            {
                GetEnemySpawnedPointXs = () => GetEnemyPositionContainer(enemyPositionMono, waveState)
                    .enemySpawnedPoints.Select(x => x.enemySpawnTransformX.position).ToList(),
                GetEnemySpawnedPointYs = () => GetEnemyPositionContainer(enemyPositionMono, waveState)
                    .enemySpawnedPoints.Select(x => x.enemySpawnTransformY.position).ToList(),
                GetEnemySpawnedEnemyEnums = () => GetEnemyPositionContainer(enemyPositionMono, waveState)
                    .enemySpawnedPoints.Select(x => x.enemyEnum).ToList(),
                GetEnemyDespawnedPoint = () => enemyPositionMono.enemyDespawnedPoint.transform.position,
                GetEnemySpawnRatios = () => GetEnemyPositionContainer(enemyPositionMono, waveState)
                    .enemySpawnedPoints.Select(x => (double)x.enemySpawnRatio).ToList()
            };
            builder.RegisterInstance(data);
        }

        static EnemySpawnedPositionContainer GetEnemyPositionContainer
            (EnemyPositionMono enemyPositionMono, WaveState waveState)
        {
            return enemyPositionMono.enemySpawnedPositionContainers[waveState.CurrentWave];
        }
    }
}