#nullable enable
using System;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawner : IStart, IUpdate
    {
        readonly IEnemyMonoBuilder _enemyMonoBuilder;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyParamsConfig _enemyParamsConfig;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IrritatedValue irritatedValue,
            IEnemyMonoBuilder enemyMonoBuilder,
            EnemyParamsConfig enemyParamsConfig)
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _irritatedValue = irritatedValue;
            _enemyParamsConfig = enemyParamsConfig;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemyParamsConfig.GetSpawnDelaySec())
            {
                SpawnEnemy();
                _timer = 0;
            }
        }

        void SpawnEnemy()
        {
            var enemyObject = _enemyMonoBuilder.Build(_enemyParamsConfig.GetSpawnedPositionRandom(), Quaternion.identity);
            IncreaseIrritatedValueByEnemy(enemyObject.EnemyEnum);
            _enemyCluster.Add(enemyObject);
        }


        void IncreaseIrritatedValueByEnemy(EnemyEnum enemy)
        {
            if (enemy == EnemyEnum.Boss) _irritatedValue.IncreaseValue(_enemyParamsConfig.GetIncreaseIrritatedValueByBoss()); 
        }
    }
}