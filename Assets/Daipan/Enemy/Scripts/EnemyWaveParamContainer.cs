#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyWaveParamContainer : IEnemyWaveParamContainer
    {
        readonly IList<EnemyWaveParamData> _enemyWaveParamDatas;
        readonly WaveState _waveState;
        public int WaveTotalCount => _enemyWaveParamDatas.Count;

        [Inject]
        public EnemyWaveParamContainer(
            EnemyParamsManager enemyParamsManager,
            WaveState waveState
        )
        {
            _enemyWaveParamDatas = CreateEnemyWaveParamData(enemyParamsManager);
            _waveState = waveState;
        }

        public EnemyWaveParamData GetEnemyWaveParamData()
        {
            return GetEnemyWaveParamData(_waveState, _enemyWaveParamDatas);
        }

        static EnemyWaveParamData GetEnemyWaveParamData(WaveState waveState,
            IList<EnemyWaveParamData> enemyWaveParamDatas)
        {
            return enemyWaveParamDatas[waveState.CurrentWave];
        }

        static List<EnemyWaveParamData> CreateEnemyWaveParamData(EnemyParamsManager enemyParamsManager)
        {
            // [Precondition]
            if (enemyParamsManager.enemyWaveParams.Count == 0)
            {
                Debug.LogWarning("EnemyWaveParams.Count is 0");
                enemyParamsManager.enemyWaveParams.Add(new EnemyWaveParam());
            }

            var enemyWaveParams = new List<EnemyWaveParamData>();
            foreach (var enemyWaveParam in enemyParamsManager.enemyWaveParams)
                enemyWaveParams.Add(new EnemyWaveParamData(enemyWaveParam));
            Debug.Log($"enemyWaveParams.Count: {enemyWaveParams.Count}");
            return enemyWaveParams;
        }
    }
}