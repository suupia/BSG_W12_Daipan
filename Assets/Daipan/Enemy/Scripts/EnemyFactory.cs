#nullable enable
using System;
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
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    // デバッグ用に好き勝手にいじるBuilder
    public class EnemyDomainBuilderCustom : IEnemyDomainBuilder
    {
        readonly EnemyAttributeParameters _attributeParameters;
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;

        public EnemyDomainBuilderCustom(
            EnemyAttributeParameters attributeParameters,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber
            )
        {
            _attributeParameters = attributeParameters;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
        }
    
        public EnemyMono SetDomain(EnemyMono enemyMono)
        {
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