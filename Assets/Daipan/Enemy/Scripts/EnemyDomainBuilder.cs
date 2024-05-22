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

    public class EnemyDomainBuilder : IEnemyDomainBuilder
    {
        readonly EnemyAttributeParameters _attributeParameters;
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;

        public EnemyDomainBuilder(
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
            var enemyEnum = DecideRandomEnemyTypeCustom();
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
        
        // 本来はScriptableObjectで制御するのでこれは後でパラメータをもらうようにして消す
        // 今はスクリプトで制御するために書いておく
        EnemyEnum DecideRandomEnemyTypeCustom()
        {
            var rand = Random.value;
            if(rand < 0.5f) return EnemyEnum.A;
            else return EnemyEnum.Boss;
        }
        
        EnemyEnum DecideRandomFromAllEnemyType()
        {
            var enemyEnums = EnemyEnum.Values;
            var rand = Random.Range(0, enemyEnums.Count());
            return enemyEnums[rand];
        }

    }
    

}