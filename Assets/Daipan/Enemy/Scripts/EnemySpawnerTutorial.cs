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
    public sealed class EnemySpawnerTutorial 
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyCluster _enemyCluster;
        readonly IEnemySpawnPoint _enemySpawnPoint;
        readonly IEnemyBuilder _enemyBuilder;
        float _timer;

        [Inject]
        public EnemySpawnerTutorial(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader, 
            EnemyCluster enemyCluster,
            IEnemySpawnPoint enemySpawnPoint,
            IEnemyBuilder enemyBuilder
        )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyCluster = enemyCluster;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyBuilder = enemyBuilder;
        }

        public void SpawnRedEnemy()
        {
            var spawnPosition = GetSpawnedPositions().LastOrDefault();
            if (spawnPosition == null)
            {
                Debug.LogWarning("Spawn position is null");
                return;
            }
            Debug.Log("Spawn Red Enemy");
            SpawnEnemy(spawnPosition, EnemyEnum.Red);
        }


        void SpawnEnemy(Vector3 spawnPosition, EnemyEnum enemyEnum)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMonoObject = _container.Instantiate(enemyMonoPrefab, spawnPosition, Quaternion.identity);
            var enemyMono = _enemyBuilder.Build(enemyEnum,enemyMonoObject);
            _enemyCluster.Add(enemyMono);
        }

        List <Vector3> GetSpawnedPositions()
        {
            var positions = _enemySpawnPoint.GetEnemySpawnedPointXs()
                .Zip(_enemySpawnPoint.GetEnemySpawnedPointYs(), (x, y) => new Vector3(x.x, y.y))
                .ToList();
            return positions;
        }

        (Vector3 spawnedPos, EnemyEnum enemyEnum) GetSpawnedPositionRandom()
        {
            var positions = GetSpawnedPositions(); 
            var enums = _enemySpawnPoint.GetEnemySpawnedEnemyEnums();
            var randomIndex = Randoms.RandomByRatios(_enemySpawnPoint.GetEnemySpawnRatios(), Random.value);
            return (positions[randomIndex], enums[randomIndex]);
        }
    }
}