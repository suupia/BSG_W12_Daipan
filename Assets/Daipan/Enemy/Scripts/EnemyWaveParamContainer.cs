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
        [Inject]
        public EnemyWaveParamContainer(
            EnemyParamsManager enemyParamsManager,
            WaveState waveState
            )
        {
            _enemyWaveParamDatas = CreateEnemyTimeLineParamData(enemyParamsManager);
            _waveState = waveState; 
        }

        public EnemyWaveParamData GetEnemyWaveParamData()
        {
            return GetEnemyWaveParamData(_waveState, _enemyWaveParamDatas);
        }
        
        static EnemyWaveParamData GetEnemyWaveParamData(WaveState waveState , IList<EnemyWaveParamData> enemyWaveParamDatas)
        {
            return enemyWaveParamDatas[waveState.CurrentWave]; 
        } 

        static List<EnemyWaveParamData> CreateEnemyTimeLineParamData(EnemyParamsManager enemyParamsManager)
        {
            // [Precondition]
            if (enemyParamsManager.enemyTimeLineParams.Count == 0)
            {
                Debug.LogWarning("EnemyTimeLineParams.Count is 0");
                enemyParamsManager.enemyTimeLineParams.Add(new EnemyWaveParam());
            }

            var enemyTimeLineParams = new List<EnemyWaveParamData>();
            foreach (var enemyTimeLineParam in enemyParamsManager.enemyTimeLineParams)
                enemyTimeLineParams.Add(new EnemyWaveParamData(enemyTimeLineParam));
            return enemyTimeLineParams;
        }
    }
}