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
    // 特定のenemyのみを生成する
    public sealed class EnemyMonoSpecificBuilder : IEnemyMonoBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;
        readonly EnemyEnum _enemyEnum;
        
        public EnemyMonoSpecificBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyEnum enemyEnum
        )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _enemyEnum = enemyEnum;
        }

        public EnemyMono Build(Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMono = _container.Instantiate(enemyMonoPrefab, position, rotation);
            Debug.Log($"_enemyEnum: {_enemyEnum}");
            enemyMono.SetDomain(new EnemyAttack(enemyMono));
            enemyMono.SetParameter(_attributeParameters.enemyParameters.First(x => x.GetEnemyEnum == _enemyEnum));
            return enemyMono;
        }

    }
}
