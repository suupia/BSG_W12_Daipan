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
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Daipan.Utility.Scripts;
using Fusion;
using R3;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawnerNetwork : IEnemySpawner 
    {
        readonly IObjectResolver _container;
        readonly NetworkRunner _runner;
        readonly IPrefabLoader<EnemyNet> _enemyMonoLoader;
        readonly EnemyCluster _enemyCluster;
        readonly IEnemySpawnPoint _enemySpawnPoint;
        readonly IEnemyBuilder _enemyBuilder;
        readonly IEnemyEnumSelector _enemyEnumSelector;
        readonly List<IDisposable> _disposables = new();
        float _timer;

        [Inject]
        public EnemySpawnerNetwork(
            IObjectResolver container
            , NetworkRunner runner
            , IPrefabLoader<EnemyNet> enemyMonoLoader
            , EnemyCluster enemyCluster
            , IEnemySpawnPoint enemySpawnPoint
            , IEnemyBuilder enemyBuilder
            , IEnemyEnumSelector enemyEnumSelector
        )
        {
            _container = container;
            _runner = runner;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyCluster = enemyCluster;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyBuilder = enemyBuilder;
            _enemyEnumSelector = enemyEnumSelector;
        }

        public void SpawnEnemy()
        {
            const float spawnRandomPositionY = 0.2f;
            var spawnPosition = GetRandomSpawnPosition(_enemySpawnPoint);
            var randomSpawnPosition = new Vector3
            {
                x = spawnPosition.x, y = spawnPosition.y + Random.Range(-spawnRandomPositionY, spawnRandomPositionY)
            };

            var enemyEnum = _enemyEnumSelector.SelectEnemyEnum();
            if (enemyEnum == EnemyEnum.RedBoss)
            {
                SpawnRedBoss(randomSpawnPosition);
            }
            else
            {
                SpawnEnemy(randomSpawnPosition, enemyEnum);
            }
        }

        void SpawnEnemy(Vector3 spawnPosition, EnemyEnum enemyEnum)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMonoObject = _runner.Spawn(enemyMonoPrefab, spawnPosition, Quaternion.identity);
            enemyMonoObject.Initialize(
                _container.Resolve<PlayerHolder>()
                , _container.Resolve<IEnemySpawnPoint>()
                , _container.Resolve<IEnemyParamContainer>()
            );
            var enemyMono = _enemyBuilder.Build(enemyMonoObject, enemyMonoObject, enemyEnum);
            _enemyCluster.Add(enemyMono);
        }

        static Vector3 GetRandomSpawnPosition(IEnemySpawnPoint enemySpawnPoint)
        {
            var positions = enemySpawnPoint.GetEnemySpawnedPointXs()
                .Zip(enemySpawnPoint.GetEnemySpawnedPointYs(), (x, y) => new Vector3(x.x, y.y))
                .ToList();
            var randomIndex = Randoms.RandomByRatios(enemySpawnPoint.GetEnemySpawnRatios(), Random.value);
            return positions[randomIndex];
        }

        void SpawnRedBoss(Vector3 spawnPosition)
        {
            const float subordinateSpawnIntervalSec = 0.3f;
            const float bossSpawnDelaySec = 0.7f;
            const int subordinateCount = 5;
            _disposables.Add(Observable.Interval(TimeSpan.FromSeconds(subordinateSpawnIntervalSec))
                .Take(subordinateCount)
                .Subscribe(
                    _ => { SpawnEnemy(spawnPosition, EnemyEnum.Red); },
                    _ =>
                    {
                        _disposables.Add(Observable.Timer(TimeSpan.FromSeconds(bossSpawnDelaySec))
                            .Subscribe(_ => { SpawnEnemy(spawnPosition, EnemyEnum.RedBoss); }));
                    }));
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables)
            {
                disposable.Dispose();
            }
        }

        ~EnemySpawnerNetwork()
        {
            Dispose();
        }
    }
}