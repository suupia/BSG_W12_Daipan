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
    public sealed class EnemyBuilderTutorial : IEnemyBuilder
    {
        readonly IEnemyParamContainer _enemyParamContainer;
        readonly EnemyCluster _enemyCluster;
        
        public EnemyBuilderTutorial(
            IEnemyParamContainer enemyParamContainer
            , EnemyCluster enemyCluster
        )
        {
            _enemyParamContainer = enemyParamContainer;
            _enemyCluster = enemyCluster;
        }

        public EnemyMono Build(EnemyMono enemyMono, EnemyEnum enemyEnum)
        {
            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamContainer.GetEnemyParamData(enemyEnum);

            enemyMono.SetDomain(
                enemyEnum,
                _enemyCluster,
                new EnemyAttackDecider(),
                new EnemyDie(enemyMono)
            );
            
            return enemyMono;
        }
        

    }
}