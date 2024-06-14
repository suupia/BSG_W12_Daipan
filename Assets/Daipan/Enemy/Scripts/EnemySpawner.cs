#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Random = UnityEngine.Random;


namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpawner : IStart, IUpdate
    {
        readonly IEnemyMonoBuilder _enemyMonoBuilder;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyParamModifyWithTimer _enemyParamModifyWithTimer;
        readonly EnemySpawnPointData _enemySpawnPointData;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        float _timer;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            EnemyCluster enemyCluster,
            IrritatedValue irritatedValue,
            IEnemyMonoBuilder enemyMonoBuilder,
            EnemyParamModifyWithTimer enemyParamModifyWithTimer,
            EnemySpawnPointData enemySpawnPointData,
            EnemyLevelDesignParamData enemyLevelDesignParamData
            )
        {
            _enemyCluster = enemyCluster;
            _enemyMonoBuilder = enemyMonoBuilder;
            _irritatedValue = irritatedValue;
            _enemyParamModifyWithTimer = enemyParamModifyWithTimer;
            _enemySpawnPointData = enemySpawnPointData;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
        }

        void IStart.Start()
        {
            // SpawnEnemy();
        }

        void IUpdate.Update()
        {
            _timer += Time.deltaTime;
            if (_timer > _enemyParamModifyWithTimer.GetSpawnDelaySec())
            {
                SpawnEnemy();
                _timer = 0;
            }
        }

        void SpawnEnemy()
        {
            var tuple = GetSpawnedPositionRandom();
            var enemyObject = _enemyMonoBuilder.Build(tuple.enemyEnum, tuple.spawnedPos , Quaternion.identity);
            _enemyCluster.Add(enemyObject);
        }

        // Todo : タプル返す、これでいいのか？
        (Vector3 spawnedPos, EnemyEnum enemyEnum) GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<EnemyEnum> enums = new();
            List<double> ratio = new();

            foreach (var point in _enemySpawnPointData.GetEnemySpawnedPointXs())
            {
                position.Add(point.enemySpawnTransformX.position);
                enums.Add(point.enemyEnum);
                ratio.Add(point.enemySpawnRatio);
            }
            Debug.Log($"ratio.Count : {ratio.Count}");
            var rand = Randoms.RandomByRatios(ratio,Random.value);
            Debug.Log($"position.Count : {position.Count}, rand : {rand}");
            return (position[rand], enums[rand]);
        }


    }
}