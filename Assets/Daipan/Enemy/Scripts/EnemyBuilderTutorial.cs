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
using Daipan.Tutorial.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyBuilderTutorial : IEnemyBuilder
    {
        readonly IEnemyParamContainer _enemyParamContainer;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyOnAttackedBuilderTutorial _enemyOnAttackedBuilder;
        readonly TutorialCurrentStep _tutorialCurrentStep;
        
        public EnemyBuilderTutorial(
            IEnemyParamContainer enemyParamContainer
            , EnemyCluster enemyCluster
            , EnemyOnAttackedBuilderTutorial enemyOnAttackedBuilder
            , TutorialCurrentStep tutorialCurrentStep
        )
        {
            _enemyParamContainer = enemyParamContainer;
            _enemyCluster = enemyCluster;
            _enemyOnAttackedBuilder = enemyOnAttackedBuilder;
            _tutorialCurrentStep = tutorialCurrentStep; 
        }

        public IEnemyMono Build(IEnemyMono enemyMono, IEnemySetDomain enemySetDomain, EnemyEnum enemyEnum)
        {
            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamContainer.GetEnemyParamData(enemyEnum);

            enemySetDomain.SetDomain(
                enemyEnum
                , _enemyCluster
                , new EnemyAttackDecider()
                , new EnemyDie(enemyMono)
                , _enemyOnAttackedBuilder.SwitchEnemyOnAttacked(enemyEnum)
                , new TutorialEnemyOnDied(enemyMono, _tutorialCurrentStep)
                
            );
            
            return enemyMono;
        }
        

    }
}