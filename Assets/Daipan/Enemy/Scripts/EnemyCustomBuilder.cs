#nullable enable
using System.Linq;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyCustomBuilder : IEnemyBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly IEnemyDomainBuilder _enemyDomainBuilder;
        
        public EnemyCustomBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            IEnemyDomainBuilder enemyDomainBuilder
            )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyDomainBuilder = enemyDomainBuilder;
        }

        public EnemyMono Build(Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMono = _container.Instantiate(enemyMonoPrefab, position, rotation);
            return _enemyDomainBuilder.SetDomain(enemyMono);
        }
        

    }
}
