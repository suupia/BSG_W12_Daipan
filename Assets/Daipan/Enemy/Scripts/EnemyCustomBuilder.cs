#nullable enable
using System.Linq;
using Daipan.Comment.MonoScripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Enemy.Scripts
{
    // デバッグように好き勝手いじるBuilder
    public sealed class EnemyCustomBuilder : IEnemyBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;
        readonly CommentSpawner _commentSpawner;
        
        public EnemyCustomBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyEnum enemyEnum,
            CommentSpawner commentSpawner
            )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _commentSpawner = commentSpawner;
        }

        public EnemyMono Build(Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyObject = _container.Instantiate(enemyMonoPrefab, position, rotation);
            var enemyEnum = DecideRandomEnemyType();
            Debug.Log($"enemyEnum: {enemyEnum}");
            enemyObject.SetParameter(_attributeParameters.enemyParameters.First(x => x.GetEnemyEnum == enemyEnum));
            enemyObject.OnDied += (sender, args) =>
            {
                if(args.IsBoss) _commentSpawner.SpawnSuperComment();
            };
            return enemyObject;
        }
        
        EnemyEnum DecideRandomEnemyType()
        {
            var rand = Random.value;
            if(rand < 0.5f) return EnemyEnum.A;
            else return EnemyEnum.Cheetah;
        }

    }
}
