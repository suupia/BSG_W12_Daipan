#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyBuilder : IEnemyBuilder
    {
        readonly IEnemyParamContainer _enemyParamContainer;
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly EnemyOnAttackedBuilder _enemyOnAttackedBuilder;
        
        public EnemyBuilder(
            IEnemyParamContainer enemyParamContainer
            , CommentSpawner commentSpawner
            , ViewerNumber viewerNumber
            , IrritatedValue irritatedValue
            , EnemyCluster enemyCluster
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , EnemyOnAttackedBuilder enemyOnAttackedBuilder
        )
        {
            _enemyParamContainer = enemyParamContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _irritatedValue = irritatedValue;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyOnAttackedBuilder = enemyOnAttackedBuilder;
        }

        public EnemyMono Build(EnemyMono enemyMono, EnemyEnum enemyEnum)
        {
            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamContainer.GetEnemyParamData(enemyEnum);

            enemyMono.SetDomain(
                enemyEnum
                ,_enemyCluster
                ,new EnemyAttackDecider()
                ,new EnemyDie(enemyMono)
               , _enemyOnAttackedBuilder.SwitchEnemyOnAttacked(enemyEnum)
            );
            
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyLevelDesignParamData.CurrentKillAmount += 1;
               
                IncreaseViewerNumber(args, _viewerNumber, _enemyLevelDesignParamData);
                SpawnComment(args, _commentSpawner);
            };
            return enemyMono;
        }
        
        static void IncreaseViewerNumber(DiedEventArgs args, ViewerNumber viewerNumber, EnemyLevelDesignParamData enemyLevelDesignParamData)
        {
            if (args.EnemyEnum.IsBoss() == false)
                viewerNumber.IncreaseViewer(enemyLevelDesignParamData.GetIncreaseViewerOnEnemyKill());
        }

        static void SpawnComment(DiedEventArgs args, CommentSpawner commentSpawner)
        {
            if (args.EnemyEnum.IsBoss() == true)
            {
                // 3倍出現
                for (var i = 0; i < 3; i++) commentSpawner.SpawnCommentByType(CommentEnum.Normal);
            }
            else
            {
                commentSpawner.SpawnCommentByType(CommentEnum.Normal);
            }
        }

   

    }
}