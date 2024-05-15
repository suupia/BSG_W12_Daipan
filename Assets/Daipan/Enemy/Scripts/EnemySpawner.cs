#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemySpawner : IStart, ITickable
    {
        readonly IEnemyBuilder _enemyBuilder;
        readonly EnemyCluster _enemyCluster;
        readonly float _spawnInterval = 1.0f;
        EnemySpawnPointMono[] _enemySpawnPoints = Array.Empty<EnemySpawnPointMono>();
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IEnemyBuilder enemyBuilder)
        {
            _enemyCluster = enemyCluster;
            _enemyBuilder = enemyBuilder;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void ITickable.Tick()
        {
            _timer += Time.deltaTime;
            if (_timer > _spawnInterval)
            {
                SpawnEnemy();
                _timer = 0;
            }
        }

        void SpawnEnemy()
        {
            _enemySpawnPoints = Object.FindObjectsByType<EnemySpawnPointMono>(FindObjectsSortMode.None);
            var enemyObject = _enemyBuilder.Build(DecideRandomSpawnPosition(), Quaternion.identity);
            _enemyCluster.AddEnemy(enemyObject);
        }

        Vector3 DecideRandomSpawnPosition()
        {
            if (_enemySpawnPoints == Array.Empty<EnemySpawnPointMono>())
            {
                Debug.LogWarning("No spawn points found");
                return Vector3.zero;
            }

            var rand = Random.Range(0, _enemySpawnPoints.Length);
            return _enemySpawnPoints[rand].transform.position;
        }
    }
}