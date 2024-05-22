#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
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
        readonly EnemyParamsServer _enemyParamsServer;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IrritatedValue irritatedValue,
            IEnemyMonoBuilder enemyMonoBuilder,
            EnemyParamsServer enemyParamsServer)
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _irritatedValue = irritatedValue;
            _enemyParamsServer = enemyParamsServer;
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
            var enemyObject = _enemyMonoBuilder.Build(_enemyParamsServer.GetSpawnedPosition(), Quaternion.identity);
            IncreaseIrritatedValueByEnemy(enemyObject.Parameter.GetEnemyEnum);
            _enemyCluster.Add(enemyObject);
        }


        void IncreaseIrritatedValueByEnemy(EnemyEnum enemy)
        {
            if (enemy == EnemyEnum.Cheetah) _irritatedValue.IncreaseValue(8); // todo : parameter もらう
        }
    }
}