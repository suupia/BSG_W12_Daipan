#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawner : IStart, IUpdate
    {
        readonly IEnemyMonoBuilder _enemyMonoBuilder;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly float _spawnInterval = 1.0f;
        EnemySpawnPointMono[] _enemySpawnPoints = Array.Empty<EnemySpawnPointMono>();
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IrritatedValue irritatedValue,
            IEnemyMonoBuilder enemyMonoBuilder)
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _irritatedValue = irritatedValue;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
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
            var enemyObject = _enemyMonoBuilder.Build(DecideRandomSpawnPosition(), Quaternion.identity);
            IncreaseIrritatedValueByEnemy(enemyObject.Parameter.GetEnemyEnum);
            _enemyCluster.Add(enemyObject);
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

        void IncreaseIrritatedValueByEnemy(EnemyEnum enemy)
        {
            if (enemy == EnemyEnum.Cheetah) _irritatedValue.IncreaseValue(8); // todo : parameter もらう
        }
    }
}