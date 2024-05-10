#nullable enable
using System;
using System.Collections.Generic;
using Stream.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Enemy
{
    public class EnemySpawner : IStartable
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;

        readonly Dictionary<ENEMY_TYPE, EnemyParameter> _enemyParameters = new();
        private EnemyMono _enemyMonoPrefab;

        [Inject]
        public EnemySpawner(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters)
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;

            foreach (var enemyParam in _attributeParameters.enemyParameters)
            {
                if(_enemyParameters.ContainsKey(enemyParam.enemyType)) continue;
                _enemyParameters.Add(enemyParam.enemyType,enemyParam);
            }
        }

        void IStartable.Start()
        {
            _enemyMonoPrefab = _enemyMonoLoader.Load();
            //Debug.Log(string.Join("\n", _attributeParameters.enemyParameters));
            SpawnEnemy(ENEMY_TYPE.A_Type);
        }


        void SpawnEnemy(ENEMY_TYPE enemyType)
        {
            var enemyObject = _container.Instantiate(_enemyMonoPrefab);
            enemyObject.PureInitialize(_enemyParameters[enemyType]);
        }
    }
}