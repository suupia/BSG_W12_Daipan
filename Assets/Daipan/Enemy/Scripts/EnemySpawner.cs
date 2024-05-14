#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemySpawner : IStartable
    {
        readonly EnemyCluster _enemyCluster;
        readonly IEnemyBuilder _enemyBuilder;
        EnemySpawnPointMono[] _enemySpawnPoints = Array.Empty<EnemySpawnPointMono>();

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IEnemyBuilder enemyBuilder)
        {
            _enemyCluster = enemyCluster;
            _enemyBuilder = enemyBuilder;
        }

        void IStartable.Start()
        {
            SpawnEnemy();
            
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