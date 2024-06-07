#nullable enable
using System.Linq;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyMonoBuilder : IEnemyMonoBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly IEnemyDomainBuilder _enemyDomainBuilder;
        
        public EnemyMonoBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            IEnemyDomainBuilder enemyDomainBuilder
            )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _enemyDomainBuilder = enemyDomainBuilder;
        }

        public EnemyMono Build(EnemyEnum enemyEnum, Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMono = _container.Instantiate(enemyMonoPrefab, position, rotation);
            return _enemyDomainBuilder.SetDomain(enemyEnum,enemyMono);
        }
        

    }
}
