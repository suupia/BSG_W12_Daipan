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
        readonly EnemyParamsManager _enemyParamsManager;
        readonly EnemyParamsConfig _enemyParamsConfig;
        readonly ViewerNumber _viewerNumber;

        public EnemyDomainBuilder(
            EnemyParamDataContainer enemyParamDataContainer,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber,
            EnemyParamsManager enemyParamsManager,
            EnemyParamsConfig enemyParamsConfig
        )
        {
            _enemyParamDataContainer = enemyParamDataContainer;
            _commentSpawner = commentSpawner;
            _viewerNumber = viewerNumber;
            _enemyParamsManager = enemyParamsManager;
            _enemyParamsConfig = enemyParamsConfig;
        }

        public EnemyMono SetDomain(EnemyMono enemyMono)
        {
            var enemyEnum = DecideRandomEnemyType();
            Debug.Log($"enemyEnum: {enemyEnum}");
            enemyMono.SetDomain(new EnemyAttackDecider(enemyMono, _enemyParamDataContainer.GetEnemyParamData(enemyEnum)));
            enemyMono.SetParameter(enemyEnum);
            enemyMono.OnDied += (sender, args) =>
            {
                // ボスを倒したときも含む
                _enemyParamsManager.currentKillAmount++;

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
            if (_enemyParamsManager.currentKillAmount >= _enemyParamsManager.spawnBossAmount)
            {
                _enemyParamsManager.currentKillAmount = 0;
                return EnemyEnum.Boss;
            }

            // 通常敵のType決め
            List<float> ratio = new();

            foreach (var enemyLife in _enemyParamsManager.enemyParams)
            {
                if (enemyLife.GetEnemyEnum == EnemyEnum.Boss) continue;
                ratio.Add(enemyLife.enemySpawnParam.spawnRatio);
            }

            // ここで100%に正規化
            ratio = EnemySpawnCalculator.NormalizeEnemySpawnRatioWithBoss(ratio,
               _enemyParamsConfig.GetEnemyTimeLineParam().spawnBossRatio);

            Debug.Log($"enemyPrams.Length : {_enemyParamsManager.enemyParams.Count}");
            Debug.Log($"Randoms.RandomByRatio(ratio) : {Randoms.RandomByRatio(ratio)}");


            var enemyEnum = _enemyParamsManager.enemyParams[Randoms.RandomByRatio(ratio)].GetEnemyEnum;
            if (enemyEnum == EnemyEnum.Boss) _enemyParamsConfig.SetCurrentKillAmount(0);
            return enemyEnum;
        }

    }
}