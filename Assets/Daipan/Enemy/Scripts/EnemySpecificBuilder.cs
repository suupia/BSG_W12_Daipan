#nullable enable
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemySpecificBuilder : IEnemyBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;
        readonly EnemyType _enemyType;
        readonly EnemyCluster _enemyCluster;
        
        public EnemySpecificBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyType enemyType,
            EnemyCluster enemyCluster
        )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _enemyType = enemyType;
            _enemyCluster = enemyCluster;
        }

        public EnemyMono Build(Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyObject = _container.Instantiate(enemyMonoPrefab, position, rotation);
            enemyObject.SetParameter(_attributeParameters.enemyParameters.First(x => x.enemyType == _enemyType),_enemyCluster);
            return enemyObject;
        }

    }
}
