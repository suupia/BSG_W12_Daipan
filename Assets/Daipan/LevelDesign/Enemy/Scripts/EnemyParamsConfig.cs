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
        readonly EnemyParamDataContainer _enemyParamDataContainer;

        [Inject]
        EnemyParamsConfig(
            EnemyParamManager enemyParamManager,
            EnemyPositionMono enemyPositionMono,
            StreamTimer streamTimer,
            EnemyParamDataContainer enemyParamDataContainer)
        {
            _enemyParamManager = enemyParamManager;
            _enemyPositionMono = enemyPositionMono;
            _streamTimer = streamTimer;
            _enemyParamDataContainer = enemyParamDataContainer;

            CheckIsValid(_enemyParamManager);
        }

        void CheckIsValid(EnemyParamManager parameters)
        {
            Debug.Log($"EnemyCount : {parameters.enemyParams.Count}");

            if (_enemyParamManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParamが設定されていません。");
                _enemyParamManager.enemyTimeLineParams.Add(new EnemyTimeLineParam());
            }
        }


        #region Params

        public double GetSpeed(EnemyEnum enemyEnum)
        {
            return _enemyParamDataContainer.GetEnemyParamData(enemyEnum).GetMoveSpeedPreSec() * GetEnemyTimeLineParam().moveSpeedRate;
        }


        public float GetSpawnDelaySec()
        {
            return GetEnemyTimeLineParam().spawnDelaySec;
        }

        public EnemyTimeLineParam GetEnemyTimeLineParam()
        {
            var timeLineParam = _enemyParamManager.enemyTimeLineParams
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