#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    public class EnemySpawnPoint : IEnemySpawnPoint
    {
        readonly EnemyPositionMono _enemyPositionMono;
        readonly WaveState _waveState; 
        public EnemySpawnPoint(EnemyPositionMono enemyPositionMono, WaveState waveState)
        {
            _enemyPositionMono = enemyPositionMono;
            _waveState = waveState;
        }
        public List<Vector3> GetEnemySpawnedPointXs()
        {
            return GetEnemyPositionContainer(_enemyPositionMono, _waveState)
                .enemySpawnedPoints.Select(x => x.enemySpawnTransformX.position).ToList();
        }

        public List<Vector3> GetEnemySpawnedPointYs()
        {
            return GetEnemyPositionContainer(_enemyPositionMono, _waveState)
                .enemySpawnedPoints.Select(x => x.enemySpawnTransformY.position).ToList();
        }

        public List<EnemyEnum> GetEnemySpawnedEnemyEnums()
        {
            return GetEnemyPositionContainer(_enemyPositionMono, _waveState)
                .enemySpawnedPoints.Select(x => x.enemyEnum).ToList();
        }

        public List<double> GetEnemySpawnRatios()
        {
            return GetEnemyPositionContainer(_enemyPositionMono, _waveState)
                .enemySpawnedPoints.Select(x => x.enemySpawnRatio).ToList();
        }

        public Vector3 GetEnemyDespawnedPoint()
        {
            return _enemyPositionMono.enemyDespawnedPoint.position;
        }
        static EnemySpawnedPositionContainer GetEnemyPositionContainer
            (EnemyPositionMono enemyPositionMono, WaveState waveState)
        {
            return enemyPositionMono.enemySpawnedPositionContainers[waveState.CurrentWave];
        }
    }
}