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
        readonly EnemyParamModifyWithTimer _enemyParamModifyWithTimer;
        readonly ViewerNumber _viewerNumber;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;

        public EnemyDomainBuilder(
            EnemyParamDataContainer enemyParamDataContainer,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber,
            EnemyParamManager enemyParamManager,
            EnemyParamModifyWithTimer enemyParamModifyWithTimer,
            EnemyCluster enemyCluster,
            EnemyLevelDesignParamData enemyLevelDesignParamData
        )
        {
            _enemyParamDataContainer = enemyParamDataContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _enemyParamManager = enemyParamManager;
            _enemyParamModifyWithTimer = enemyParamModifyWithTimer;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
        }

        public EnemyMono SetDomain(NewEnemyType enemyEnum, EnemyMono enemyMono)
        {
            if(enemyEnum == NewEnemyType.None) enemyEnum = DecideRandomEnemyType();
            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamDataContainer.GetEnemyParamData(enemyEnum);
            enemyMono.SetDomain(
                enemyEnum,
                new EnemyHp(enemyParamData.GetCurrentHp(), enemyMono, _enemyCluster),
                new EnemyAttackDecider(enemyMono, enemyParamData, new EnemyAttack(enemyParamData))
            );
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyLevelDesignParamData.SetCurrentKillAmount(_enemyLevelDesignParamData.GetCurrentKillAmount() + 1);

                if (!args.IsBoss) _viewerNumber.IncreaseViewer(_enemyLevelDesignParamData.GetIncreaseViewerOnEnemyKill()); // todo :パラメータを設定できるようにする

                if (args.IsBoss || args.IsQuickDefeat) _commentSpawner.SpawnCommentByType(CommentEnum.Normal);
            };
            return enemyMono;
        }

        // 本来はScriptableObjectで制御するのでこれは後でパラメータをもらうようにして消す
        // 今はスクリプトで制御するために書いておく
        NewEnemyType DecideRandomEnemyTypeCustom()
        {
            var rand = Random.value;
            if (rand < 0.5f) return NewEnemyType.A;
            return NewEnemyType.Boss;
        }


        NewEnemyType DecideRandomEnemyType()
        {
            // ボス発生条件を満たしていればBOSSを生成
            if (_enemyLevelDesignParamData.GetCurrentKillAmount() >= _enemyLevelDesignParamData.GetSpawnBossAmount())
            {
                _enemyLevelDesignParamData.SetCurrentKillAmount(0);
                return NewEnemyType.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyParamManager.enemyParams)
            {
                if (enemyLife.EnemyType == NewEnemyType.Boss) continue;
                ratio.Add(enemyLife.enemySpawnParam.spawnRatio);
            }

            // ここで100%に正規化
            ratio = EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio,
                (float)_enemyParamModifyWithTimer.GetSpawnBossPercent());

            Debug.Log($"enemyPrams.Length : {_enemyParamManager.enemyParams.Count}");
            Debug.Log($"Randoms.RandomByRatio(ratio) : {Randoms.RandomByRatio(ratio)}");


            var enemyEnum = _enemyParamManager.enemyParams[Randoms.RandomByRatio(ratio)].EnemyType;
            if (enemyEnum == NewEnemyType.Boss) _enemyLevelDesignParamData.SetCurrentKillAmount(0);
            return enemyEnum;
        }
    }
}