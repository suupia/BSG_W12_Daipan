#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Enemy.Scripts
{
    public class EnemyBuilder : IEnemyBuilder
    {
        readonly EnemyParamDataContainer _enemyParamDataContainer;
        readonly CommentSpawner _commentSpawner;
        readonly EnemyParamManager _enemyParamManager;
        readonly ViewerNumber _viewerNumber;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly IEnemyTimeLineParamContainer _enemyTimeLineParamContainer;
        public EnemyBuilder(
            EnemyParamDataContainer enemyParamDataContainer,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber,
            IrritatedValue irritatedValue,
            EnemyParamManager enemyParamManager,
            EnemyCluster enemyCluster,
            EnemyLevelDesignParamData enemyLevelDesignParamData,
            IEnemyTimeLineParamContainer enemyTimeLineParamContainer
        )
        {
            _enemyParamDataContainer = enemyParamDataContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _irritatedValue = irritatedValue;
            _enemyParamManager = enemyParamManager;
            _enemyCluster = enemyCluster;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _enemyTimeLineParamContainer = enemyTimeLineParamContainer;
        }

        public EnemyMono Build(EnemyEnum enemyEnum, EnemyMono enemyMono)
        {
            if (enemyEnum == EnemyEnum.None) enemyEnum = DecideRandomEnemyType(); // EnemyEnum.Noneが設定されていない時の処理

            if (IsSpawnBoss()) enemyEnum = EnemyEnum.RedBoss;

            Debug.Log($"enemyEnum: {enemyEnum}");
            var enemyParamData = _enemyParamDataContainer.GetEnemyParamData(enemyEnum);
            enemyMono.SetDomain(
                enemyEnum,
                new EnemyHp(enemyParamData.GetCurrentHp(), enemyMono, _enemyCluster),
                new EnemyAttackDecider(enemyMono, enemyParamData, new EnemyAttack(enemyParamData)),
                new EnemySuicideAttack(enemyMono,enemyParamData),
                new EnemyDied(_enemyCluster, enemyMono)
            );
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyLevelDesignParamData.SetCurrentKillAmount(_enemyLevelDesignParamData.GetCurrentKillAmount() + 1);

                if (args.enemyEnum.IsSpecial() == true)
                    _irritatedValue.IncreaseValue(enemyParamData.GetIrritationAfterKill());

                if (args.enemyEnum.IsBoss() == false)
                    _viewerNumber.IncreaseViewer(_enemyLevelDesignParamData
                        .GetIncreaseViewerOnEnemyKill()); // todo :パラメータを設定できるようにする

                if (args.enemyEnum.IsBoss() == true) _commentSpawner.SpawnCommentByType(CommentEnum.Normal);
            };
            return enemyMono;
        }

        // 本来はScriptableObjectで制御するのでこれは後でパラメータをもらうようにして消す
        // 今はスクリプトで制御するために書いておく
        EnemyEnum DecideRandomEnemyTypeCustom()
        {
            var rand = Random.value;
            if (rand < 0.5f) return EnemyEnum.Blue;
            return EnemyEnum.RedBoss;
        }

        bool IsSpawnBoss()
        {
            // ボスが出現する条件1
            if (_enemyLevelDesignParamData.GetCurrentKillAmount() >= _enemyLevelDesignParamData.GetSpawnBossAmount())
            {
                _enemyLevelDesignParamData.SetCurrentKillAmount(0);
                return true;
            }

            // ボスが出現する条件2
            if (Random.value < _enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent() / 100.0) return true;

            return false;
        }


        EnemyEnum DecideRandomEnemyType()
        {
            // BOSSをスポーンするかどうかの判定
            if (Random.value < _enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent() / 100.0) return EnemyEnum.RedBoss;

            // 通常敵のType決め
            List<double> ratio = new();
            foreach (var enemyLife in _enemyParamManager.enemyParams)
            {
                if (enemyLife.enemyEnum == EnemyEnum.RedBoss) continue;
                ratio.Add(enemyLife.enemySpawnParam.spawnRatio);
            }

            // ここで100%に正規化
            ratio = EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio,
                _enemyTimeLineParamContainer.GetEnemyTimeLineParamData().GetSpawnBossPercent());
            Debug.Log($"enemyPrams.Length : {_enemyParamManager.enemyParams.Count}");
            var enemyEnum = _enemyParamManager.enemyParams[Randoms.RandomByRatios(ratio, Random.value)].enemyEnum;
            if (enemyEnum == EnemyEnum.RedBoss) _enemyLevelDesignParamData.SetCurrentKillAmount(0);
            return enemyEnum;
        }
    }
}