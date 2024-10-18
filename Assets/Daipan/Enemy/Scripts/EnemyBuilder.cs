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
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyBuilder : IEnemyBuilder
    {
        readonly CommentSpawner _commentSpawner;
        readonly ViewerNumber _viewerNumber;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly EnemyOnAttackedBuilder _enemyOnAttackedBuilder;
        
        readonly ComboSpawner _comboSpawner;
        readonly ComboCounter _comboCounter;
        
        public EnemyBuilder(
             CommentSpawner commentSpawner
            , ViewerNumber viewerNumber
            , EnemyCluster enemyCluster
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , EnemyOnAttackedBuilder enemyOnAttackedBuilder
            , ComboSpawner comboSpawner
            , ComboCounter comboCounter
        )
        {
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyOnAttackedBuilder = enemyOnAttackedBuilder;
            _comboSpawner = comboSpawner;
            _comboCounter = comboCounter;
        }

        public IEnemyMono Build(IEnemyMono enemyMono, IEnemySetDomain enemySetDomain, EnemyEnum enemyEnum)
        {

            enemySetDomain.SetDomain(
                enemyEnum
                ,_enemyCluster
                , new EnemyAttackDecider()
                , new EnemyDie(enemyMono)
                , WrapWithComboSpawner(_enemyOnAttackedBuilder.SwitchEnemyOnAttacked(enemyEnum), enemyMono)
                , new NoneEnemyOnDied()
            );
            
            enemyMono.OnDiedEvent += (sender, args) =>
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
 
        IEnemyOnAttacked WrapWithComboSpawner(IEnemyOnAttacked enemyOnAttacked, IEnemyMono enemyMono) 
        {
            return new EnemyOnAttackedWithComboSpawner(enemyOnAttacked, enemyMono, _comboSpawner, _comboCounter);
        }
        
        public class EnemyOnAttackedWithComboSpawner : IEnemyOnAttacked
        {
            readonly IEnemyOnAttacked _enemyOnAttacked;
            readonly IEnemyMono _enemyMono;
            readonly ComboSpawner _comboSpawner;
            readonly ComboCounter _comboCounter;
            public EnemyOnAttackedWithComboSpawner(
                IEnemyOnAttacked enemyOnAttacked
               , IEnemyMono enemyMono
                    , ComboSpawner comboSpawner
                    , ComboCounter comboCounter )
            {
                _enemyMono = enemyMono;
                _enemyOnAttacked = enemyOnAttacked;
                _comboSpawner = comboSpawner;
                _comboCounter = comboCounter;
            }

            public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
            {
                var newHp = _enemyOnAttacked.OnAttacked(hp, playerParamData);
                if (newHp.Value < hp.Value)
                {
                    _comboCounter.IncreaseCombo();
                    _comboSpawner.SpawnCombo(_comboCounter.ComboCount, _enemyMono.Transform.position); 
                }
                return newHp;
            }
        } 
   

    }
}