#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
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
        readonly EnemyParamsManager _enemyParamsManager;
        readonly ViewerNumber _viewerNumber;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly IEnemyTimeLineParamContainer _enemyTimeLineParamContainer;
        
        public EnemyBuilder(
            IEnemyParamContainer enemyParamContainer
            , CommentSpawner commentSpawner
            , ViewerNumber viewerNumber
            , IrritatedValue irritatedValue
            , EnemyParamsManager enemyParamsManager
            , EnemyCluster enemyCluster
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , IEnemyTimeLineParamContainer enemyTimeLineParamContainer
        )
        {
            _enemyParamContainer = enemyParamContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _irritatedValue = irritatedValue;
            _enemyParamsManager = enemyParamsManager;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyTimeLineParamContainer = enemyTimeLineParamContainer;
        }

        public EnemyMono Build(EnemyMono enemyMono)
        {
            var enemyEnum = IsSpawnBoss(_enemyLevelDesignParamData, _enemyTimeLineParamContainer) 
                ? DecideRandomEnemyBossType(_enemyParamsManager) : DecideRandomEnemyNormalType(_enemyParamsManager);

            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamContainer.GetEnemyParamData(enemyEnum);

            enemyMono.SetDomain(
                enemyEnum,
                _enemyCluster,
                new EnemyAttackDecider(),
                new EnemySuicideAttack(enemyMono,enemyParamData),
                new EnemyDied(enemyMono)
            );
            
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyLevelDesignParamData.SetCurrentKillAmount(_enemyLevelDesignParamData.GetCurrentKillAmount() + 1);
               
                IncreaseIrritatedValue(args, _irritatedValue, enemyParamData);
                IncreaseViewerNumber(args, _viewerNumber, _enemyLevelDesignParamData);
                SpawnComment(args, _commentSpawner);
            };
            return enemyMono;
        }
        
        static void IncreaseIrritatedValue(DiedEventArgs args, IrritatedValue irritatedValue, IEnemyParamData enemyParamData)
        {
            if (args.EnemyEnum.IsSpecial() == true)
                irritatedValue.IncreaseValue(enemyParamData.GetIrritationAfterKill());
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

        static bool IsSpawnBoss(
            EnemyLevelDesignParamData enemyLevelDesignParamData
            , IEnemyTimeLineParamContainer enemyTimeLineParamContainer
            ) 
        {
            // ボスが出現する条件1
            if (enemyLevelDesignParamData.GetCurrentKillAmount() >= enemyLevelDesignParamData.GetSpawnBossAmount())
            {
                enemyLevelDesignParamData.SetCurrentKillAmount(0);
                return true;
            }

            // ボスが出現する条件2
            if (Random.value < enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent() / 100.0) return true;

            return false;
        }

        static EnemyEnum DecideRandomEnemyNormalType(EnemyParamsManager enemyParamsManager)
        {
           List<(EnemyEnum EnemyEnum, double Ratio)> table =  enemyParamsManager.enemyParams
                .Where(x => x.enemyEnum.IsBoss() != true)
                .Select(x => (x.enemyEnum, x.enemySpawnParam.spawnRatio))
                .ToList();
           var randomIndex = Randoms.RandomByRatios(table.Select(x => x.Ratio).ToList(), Random.value);
           return  table[randomIndex].EnemyEnum;
        }
        
        static EnemyEnum DecideRandomEnemyBossType(EnemyParamsManager enemyParamsManager)
        {
            List<(EnemyEnum EnemyEnum, double Ratio)> table =  enemyParamsManager.enemyParams
                .Where(x => x.enemyEnum.IsBoss() == true)
                .Select(x => (x.enemyEnum, x.enemySpawnParam.spawnRatio))
                .ToList();
            var randomIndex = Randoms.RandomByRatios(table.Select(x => x.Ratio).ToList(), Random.value);
            return  table[randomIndex].EnemyEnum;
        }
    }
}