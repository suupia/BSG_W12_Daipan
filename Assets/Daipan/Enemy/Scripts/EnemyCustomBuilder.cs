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
    // デバッグように好き勝手いじるBuilder
    public sealed class EnemyCustomBuilder : IEnemyBuilder
    {
        readonly IObjectResolver _container;
        readonly IPrefabLoader<EnemyMono> _enemyMonoLoader;
        readonly EnemyAttributeParameters _attributeParameters;
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;
        
        public EnemyCustomBuilder(
            IObjectResolver container,
            IPrefabLoader<EnemyMono> enemyMonoLoader,
            EnemyAttributeParameters attributeParameters,
            EnemyEnum enemyEnum,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber
            )
        {
            _container = container;
            _enemyMonoLoader = enemyMonoLoader;
            _attributeParameters = attributeParameters;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
        }

        public EnemyMono Build(Vector3 position, Quaternion rotation)
        {
            var enemyMonoPrefab = _enemyMonoLoader.Load();
            var enemyMono = _container.Instantiate(enemyMonoPrefab, position, rotation);
            var enemyEnum = DecideRandomEnemyType();
            Debug.Log($"enemyEnum: {enemyEnum}");
            enemyMono.SetDomain(new EnemyAttack(enemyMono));
            enemyMono.SetParameter(_attributeParameters.enemyParameters.First(x => x.GetEnemyEnum == enemyEnum));
            enemyMono.OnDied += (sender, args) =>
            {
                if(!args.IsBoss) _viewerNumber.IncreaseViewer(7);
                if(args.IsBoss) _commentSpawner.SpawnComment(CommentEnum.Super);
            };
            return enemyMono;
        }
        
        EnemyEnum DecideRandomEnemyType()
        {
            var rand = Random.value;
            if(rand < 0.5f) return EnemyEnum.A;
            else return EnemyEnum.Cheetah;
        }

    }
}
