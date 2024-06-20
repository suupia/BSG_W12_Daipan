#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawner : IStart, IUpdate
    {
        readonly IEnemyMonoBuilder _enemyMonoBuilder;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyParamModifyWithTimer _enemyParamModifyWithTimer;
        readonly EnemySpawnPointData _enemySpawnPointData;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IEnemyMonoBuilder enemyMonoBuilder,
            EnemyParamModifyWithTimer enemyParamModifyWithTimer,
            EnemySpawnPointData enemySpawnPointData
        )
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _enemyParamModifyWithTimer = enemyParamModifyWithTimer;
            _enemySpawnPointData = enemySpawnPointData;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemyParamModifyWithTimer.GetSpawnIntervalSec())
            {
                SpawnEnemy();
                _timer = 0;
            }
        }

        void SpawnEnemy()
        {
            var tuple = GetSpawnedPositionRandom();
            var enemyObject = _enemyMonoBuilder.Build(tuple.enemyEnum, tuple.spawnedPos, Quaternion.identity);
            _enemyCluster.Add(enemyObject);
        }

        (Vector3 spawnedPos, EnemyEnum enemyEnum) GetSpawnedPositionRandom()
        {
            var positions = _enemySpawnPointData.GetEnemySpawnedPointXs()
                .Zip(_enemySpawnPointData.GetEnemySpawnedPointYs(), (x, y) => new Vector3(x.x, y.y))
                .ToList();
            var enums = _enemySpawnPointData.GetEnemySpawnedEnemyEnums();
            
            var randomIndex = Randoms.RandomByRatios(_enemySpawnPointData.GetEnemySpawnRatios(), Random.value);
            return (positions[randomIndex], enums[randomIndex]);
        }
        
    }
}