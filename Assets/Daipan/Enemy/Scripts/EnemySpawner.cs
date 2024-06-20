#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawner : IStart, IUpdate
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyCluster _enemyCluster;
        readonly IEnemySpawnPoint _enemySpawnPoint;
        readonly IEnemyTimeLineParamContainer _enemyTimeLInePramContainer;
        readonly float _spawnRandomPositionY = 0.2f;
        readonly IEnemyBuilder _enemyBuilder;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader, 
            EnemyCluster enemyCluster,
            IEnemySpawnPoint enemySpawnPoint,
            IEnemyTimeLineParamContainer enemyTimeLInePramContainer,
            IEnemyBuilder enemyBuilder
        )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyCluster = enemyCluster;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyTimeLInePramContainer = enemyTimeLInePramContainer;
            _enemyBuilder = enemyBuilder;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemyTimeLInePramContainer.GetEnemyTimeLineParamData().GetSpawnIntervalSec())
            {
                SpawnEnemy();
                _timer = 0;
            }
        }

        void SpawnEnemy()
        {
            var tuple = GetSpawnedPositionRandom();
            var spawnPosition = new Vector3 { x = tuple.spawnedPos.x, y = tuple.spawnedPos.y + Random.Range(-_spawnRandomPositionY, _spawnRandomPositionY) };
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMonoObject = _container.Instantiate(enemyMonoPrefab, spawnPosition, Quaternion.identity);
            var enemyMono = _enemyBuilder.Build(tuple.enemyEnum,enemyMonoObject);
            _enemyCluster.Add(enemyMono);
        }

        (Vector3 spawnedPos, EnemyEnum enemyEnum) GetSpawnedPositionRandom()
        {
            var positions = _enemySpawnPoint.GetEnemySpawnedPointXs()
                .Zip(_enemySpawnPoint.GetEnemySpawnedPointYs(), (x, y) => new Vector3(x.x, y.y))
                .ToList();
            var enums = _enemySpawnPoint.GetEnemySpawnedEnemyEnums();
            var randomIndex = Randoms.RandomByRatios(_enemySpawnPoint.GetEnemySpawnRatios(), Random.value);
            return (positions[randomIndex], enums[randomIndex]);
        }
    }
}