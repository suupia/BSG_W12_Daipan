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
        readonly EnemySpawnPointData _enemySpawnPointData;
        readonly EnemyTimeLineParamWrapContainer _enemyTimeLInePramWrapContainer;
        readonly StreamTimer _streamTimer;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IEnemyMonoBuilder enemyMonoBuilder,
            EnemySpawnPointData enemySpawnPointData,
            EnemyTimeLineParamWrapContainer enemyTimeLInePramWrapContainer,
            StreamTimer streamTimer
        )
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _enemySpawnPointData = enemySpawnPointData;
            _enemyTimeLInePramWrapContainer = enemyTimeLInePramWrapContainer;
            _streamTimer = streamTimer;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemyTimeLInePramWrapContainer.GetEnemyTimeLineParamData(_streamTimer).GetSpawnIntervalSec())
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