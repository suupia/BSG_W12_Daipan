#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemyDomainBuilder : IEnemyDomainBuilder
    {
        readonly EnemyParamDataContainer _enemyParamDataContainer;
        readonly CommentSpawner _commentSpawner;
        readonly EnemyParamManager _enemyParamManager;
        readonly EnemyParamsConfig _enemyParamsConfig;
        readonly ViewerNumber _viewerNumber;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;

        public EnemyDomainBuilder(
            EnemyParamDataContainer enemyParamDataContainer,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber,
            EnemyParamManager enemyParamManager,
            EnemyParamsConfig enemyParamsConfig,
            EnemyCluster enemyCluster,
            EnemyLevelDesignParamData enemyLevelDesignParamData
        )
        {
            _enemyParamDataContainer = enemyParamDataContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _enemyParamManager = enemyParamManager;
            _enemyParamsConfig = enemyParamsConfig;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
        }

        public EnemyMono SetDomain(EnemyMono enemyMono)
        {
            var enemyEnum = DecideRandomEnemyType();
            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamDataContainer.GetEnemyParamData(enemyEnum);
            enemyMono.SetDomain(
                enemyEnum,
                new EnemyHp(enemyParamData.GetCurrentHp(),enemyMono, _enemyCluster),
                new EnemyAttackDecider(enemyMono, enemyParamData, new EnemyAttack(enemyParamData))
                );
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyLevelDesignParamData.SetCurrentKillAmount(_enemyLevelDesignParamData.GetCurrentKillAmount() + 1);

                if (!args.IsBoss) _viewerNumber.IncreaseViewer(7);
                // if(args.IsBoss) _commentSpawner.SpawnCommentByType(CommentEnum.Super);
                // else _commentSpawner.SpawnCommentByType(CommentEnum.Normal);

                if (args.IsBoss || args.IsQuickDefeat) _commentSpawner.SpawnCommentByType(CommentEnum.Normal);
            };
            return enemyMono;
        }

        // 本来はScriptableObjectで制御するのでこれは後でパラメータをもらうようにして消す
        // 今はスクリプトで制御するために書いておく
        EnemyEnum DecideRandomEnemyTypeCustom()
        {
            var rand = Random.value;
            if (rand < 0.5f) return EnemyEnum.A;
            return EnemyEnum.Boss;
        }

        EnemyEnum DecideRandomFromAllEnemyType()
        {
            var enemyEnums = EnemyEnum.Values;
            var rand = Random.Range(0, enemyEnums.Count());
            return enemyEnums[rand];
        }
        
        
   
        EnemyEnum DecideRandomEnemyType()
        {
            // ボス発生条件を満たしていればBOSSを生成
            if (_enemyLevelDesignParamData.GetCurrentKillAmount() >= _enemyLevelDesignParamData.GetSpawnBossAmount())
            {
                _enemyLevelDesignParamData.SetCurrentKillAmount(0);
                return EnemyEnum.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyParamManager.enemyParams)
            {
                if (enemyLife.GetEnemyEnum == EnemyEnum.Boss) continue;
                ratio.Add(enemyLife.enemySpawnParam.spawnRatio);
            }

            // ここで100%に正規化
            ratio = EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio,
               _enemyParamsConfig.GetEnemyTimeLineParam().spawnBossPercent);

            Debug.Log($"enemyPrams.Length : {_enemyParamManager.enemyParams.Count}");
            Debug.Log($"Randoms.RandomByRatio(ratio) : {Randoms.RandomByRatio(ratio)}");


            var enemyEnum = _enemyParamManager.enemyParams[Randoms.RandomByRatio(ratio)].GetEnemyEnum;
            if (enemyEnum == EnemyEnum.Boss) _enemyLevelDesignParamData.SetCurrentKillAmount(0);
            return enemyEnum;
        }

    }
}