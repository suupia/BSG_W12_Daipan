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
using Daipan.Transporter;
using Daipan.Transporter.Scripts;
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
    public sealed class EnemySpawnerNetwork : IEnemySpawner, IEnemySpawnerNetwork
    {
        readonly IObjectResolver _container;
        readonly NetworkRunner _runner;
        readonly IPrefabLoader<EnemyNet> _enemyMonoLoader;
        readonly IEnemyCluster _enemyCluster;
        readonly EnemyClusterNetwork _enemyClusterNetwork;
        readonly IEnemySpawnPoint _enemySpawnPoint;
        readonly IEnemyBuilder _enemyBuilder;
        readonly IEnemyEnumSelector _enemyEnumSelector;
        readonly List<IDisposable> _disposables = new();
        readonly PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper;
        float _timer;

        [Inject]
        public EnemySpawnerNetwork(
            IObjectResolver container
            , NetworkRunner runner
            , IPrefabLoader<EnemyNet> enemyMonoLoader
            , IEnemyCluster enemyCluster
            , EnemyClusterNetwork enemyClusterNetwork
            , IEnemySpawnPoint enemySpawnPoint
            , IEnemyBuilder enemyBuilder
            , IEnemyEnumSelector enemyEnumSelector
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
        )
        {
            _container = container;
            _runner = runner;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyCluster = enemyCluster;
            _enemyClusterNetwork = enemyClusterNetwork;
            _enemySpawnPoint = enemySpawnPoint;
            _enemyBuilder = enemyBuilder;
            _enemyEnumSelector = enemyEnumSelector;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
        }

        public void SpawnEnemy()
        {
            // EnemyのSpawnはStreamerが行う
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            const float spawnRandomPositionY = 0.2f;
            var spawnPosition = GetRandomSpawnPosition(_enemySpawnPoint);
            var randomSpawnPosition = new Vector3
            {
                x = spawnPosition.x,
                y = spawnPosition.y + Random.Range(-spawnRandomPositionY, spawnRandomPositionY)
            };

            var enemyEnum = _enemyEnumSelector.SelectEnemyEnum();
            if (enemyEnum == EnemyEnum.RedBoss)
            {
                SpawnRedBoss(randomSpawnPosition, PlayerRef.None);
            }
            else
            {
                SpawnEnemy(randomSpawnPosition, enemyEnum, PlayerRef.None);
            }
        }

        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            if (enemyEnum == EnemyEnum.None) return;
            // EnemyのSpawnはStreamerが行う
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            const float spawnRandomPositionY = 0.2f;
            var spawnPosition = GetRandomSpawnPosition(_enemySpawnPoint);
            var randomSpawnPosition = new Vector3
            {
                x = spawnPosition.x,
                y = spawnPosition.y + Random.Range(-spawnRandomPositionY, spawnRandomPositionY)
            };

            if (enemyEnum == EnemyEnum.RedBoss)
            {
                SpawnRedBoss(randomSpawnPosition, PlayerRef.None);
            }
            else
            {
                SpawnEnemy(randomSpawnPosition, enemyEnum, PlayerRef.None);
            }
        }

        public void SpawnEnemy(EnemyEnum enemyEnum, PlayerRef playerRef)
        {
            if (enemyEnum == EnemyEnum.None) return;
            // EnemyのSpawnはStreamerが行う
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            const float spawnRandomPositionY = 0.2f;
            var spawnPosition = GetRandomSpawnPosition(_enemySpawnPoint);
            var randomSpawnPosition = new Vector3
            {
                x = spawnPosition.x,
                y = spawnPosition.y + Random.Range(-spawnRandomPositionY, spawnRandomPositionY)
            };

            if (enemyEnum == EnemyEnum.RedBoss)
            {
                SpawnRedBoss(randomSpawnPosition, playerRef);
            }
            else
            {
                SpawnEnemy(randomSpawnPosition, enemyEnum, playerRef);
            }
        }


        void SpawnEnemy(Vector3 spawnPosition, EnemyEnum enemyEnum, PlayerRef playerRef)
        {
            // EnemyのSpawnはStreamerが行う
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMonoObject = _runner.Spawn(enemyMonoPrefab, spawnPosition, Quaternion.identity,
                onBeforeSpawned: (runner, obj) =>
                {
                    var enemyMono = obj.GetComponent<EnemyNet>();
                    enemyMono.Initialize(
                        _container.Resolve<PlayerHolder>()
                        , _container.Resolve<IEnemySpawnPoint>()
                        , _container.Resolve<IEnemyParamContainer>()
                    );
                    _enemyBuilder.Build(enemyMono, enemyEnum)(enemyMono);
                    _enemyClusterNetwork.SetPlayerRef(enemyMono, playerRef);
                });
            _enemyCluster.Add(enemyMonoObject);
        }


        static Vector3 GetRandomSpawnPosition(IEnemySpawnPoint enemySpawnPoint)
        {
            var positions = enemySpawnPoint.GetEnemySpawnedPointXs()
                .Zip(enemySpawnPoint.GetEnemySpawnedPointYs(), (x, y) => new Vector3(x.x, y.y))
                .ToList();
            var randomIndex = Randoms.RandomByRatios(enemySpawnPoint.GetEnemySpawnRatios(), Random.value);
            return positions[randomIndex];
        }

        void SpawnRedBoss(Vector3 spawnPosition, PlayerRef playerRef)
        {
            const float subordinateSpawnIntervalSec = 0.3f;
            const float bossSpawnDelaySec = 0.7f;
            const int subordinateCount = 5;
            _disposables.Add(Observable.Interval(TimeSpan.FromSeconds(subordinateSpawnIntervalSec))
                .Take(subordinateCount)
                .Subscribe(
                    _ => { SpawnEnemy(spawnPosition, EnemyEnum.Red, playerRef); },
                    _ =>
                    {
                        _disposables.Add(Observable.Timer(TimeSpan.FromSeconds(bossSpawnDelaySec))
                            .Subscribe(_ => { SpawnEnemy(spawnPosition, EnemyEnum.RedBoss, playerRef); }));
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