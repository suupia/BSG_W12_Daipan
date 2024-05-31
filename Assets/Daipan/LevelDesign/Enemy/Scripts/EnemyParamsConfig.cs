#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamsConfig
    {
        readonly EnemyParamsManager _enemyParamsManager;
        readonly EnemyPositionMono _enemyPositionMono;
        readonly Timer _timer;

        [Inject]
        EnemyParamsConfig(
            EnemyParamsManager enemyParamsManager,
            EnemyPositionMono enemyPositionMono,
            Timer timer)
        {
            _enemyParamsManager = enemyParamsManager;
            _enemyPositionMono = enemyPositionMono;
            _timer = timer;

            CheckIsValid(_enemyParamsManager);
        }

        void CheckIsValid(EnemyParamsManager parameters)
        {
            Debug.Log($"EnemeyCount : {parameters.enemyParams.Count}");
            foreach (var enemyParam in parameters.enemyParams)
            {
                if (enemyParam.enemyAttackParam.attackAmount <= 0)
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃力が0以下です。");
                if (enemyParam.enemyAttackParam.attackRange <= 0)
                    Debug.LogWarning($"{enemyParam.GetEnemyEnum}の攻撃範囲が0以下です。");
            }

            if (_enemyParamsManager.enemyTimeLines.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParamが設定されていません。");
                _enemyParamsManager.enemyTimeLines.Add(new EnemyTimeLineParam());
            }
        }


        #region Params

        public float GetSpeed(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).enemyMoveParam.moveSpeedPerSec * GetEnemyTimeLineParam().moveSpeedRate;
        }


        public Sprite GetSprite(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).sprite;
        }

        public float GetSpawnDelaySec()
        {
            return GetEnemyTimeLineParam().spawnDelaySec;
        }
        
        public void SetCurrentKillAmount(int amount)
        {
            _enemyParamsManager.currentKillAmount = amount;
        }
        

        EnemyParam GetEnemyParams(EnemyEnum enemyEnum)
        {
            return _enemyParamsManager.enemyParams.First(c => c.GetEnemyEnum == enemyEnum);
        }

       public  EnemyTimeLineParam GetEnemyTimeLineParam()
        {
            var timeLineParam = _enemyParamsManager.enemyTimeLines
                .Where(e => e.startTime <= _timer.GetCurrentTime())
                .OrderByDescending(e => e.startTime).First();
            return timeLineParam;
        }

        #endregion

        #region Position

        public Vector3 GetSpawnedPositionRandom()
        {
            List<Vector3> position = new();
            List<float> ratio = new();

            foreach (var point in _enemyPositionMono.enemySpawnedPoints)
            {
                position.Add(point.transform.position);
                ratio.Add(point.ratio);
            }

            return position[Randoms.RandomByRatio(ratio)];
        }

        public Vector3 GetDespawnedPosition()
        {
            return _enemyPositionMono.enemyDespawnedPoint.position;
        }

        #endregion
    }

}