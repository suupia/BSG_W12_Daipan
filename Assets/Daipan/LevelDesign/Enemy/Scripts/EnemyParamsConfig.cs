#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamsConfig
    {
        readonly EnemyParamManager _enemyParamManager;
        readonly EnemyPositionMono _enemyPositionMono;
        readonly StreamTimer _streamTimer;

        [Inject]
        EnemyParamsConfig(
            EnemyParamManager enemyParamManager,
            EnemyPositionMono enemyPositionMono,
            StreamTimer streamTimer)
        {
            _enemyParamManager = enemyParamManager;
            _enemyPositionMono = enemyPositionMono;
            _streamTimer = streamTimer;

            CheckIsValid(_enemyParamManager);
        }

        void CheckIsValid(EnemyParamManager parameters)
        {
            Debug.Log($"EnemyCount : {parameters.enemyParams.Count}");

            if (_enemyParamManager.enemyTimeLines.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParamが設定されていません。");
                _enemyParamManager.enemyTimeLines.Add(new EnemyTimeLineParam());
            }
        }


        #region Params

        public float GetSpeed(EnemyEnum enemyEnum)
        {
            return GetEnemyParams(enemyEnum).enemyMoveParam.moveSpeedPerSec * GetEnemyTimeLineParam().moveSpeedRate;
        }


        public float GetSpawnDelaySec()
        {
            return GetEnemyTimeLineParam().spawnDelaySec;
        }


        EnemyParam GetEnemyParams(EnemyEnum enemyEnum)
        {
            return _enemyParamManager.enemyParams.First(c => c.GetEnemyEnum == enemyEnum);
        }

        public EnemyTimeLineParam GetEnemyTimeLineParam()
        {
            var timeLineParam = _enemyParamManager.enemyTimeLines
                .Where(e => e.startTime <= _streamTimer.CurrentTime)
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