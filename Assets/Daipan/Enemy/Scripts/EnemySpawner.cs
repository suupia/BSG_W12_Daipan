#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
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
        readonly EnemyAttributeParameters _attributeParameters;
        readonly IObjectResolver _container;
        readonly EnemyCluster _enemyCluster;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        EnemySpawnPointMono[] _enemySpawnPoints = Array.Empty<EnemySpawnPointMono>();

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyCluster enemyCluster)
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _enemyCluster = enemyCluster;
        }

        void IStartable.Start()
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            _enemySpawnPoints = Object.FindObjectsByType<EnemySpawnPointMono>(FindObjectsSortMode.None);
            SpawnEnemy(EnemyType.A, enemyMonoPrefab);
            
        }


        void SpawnEnemy(EnemyType enemyType, EnemyMono enemyMonoPrefab)
        {
            var enemyObject = _container.Instantiate(enemyMonoPrefab, DecideRandomSpawnPosition(), Quaternion.identity);
            enemyObject.PureInitialize(_attributeParameters.enemyParameters.First(x => x.enemyType == enemyType));

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