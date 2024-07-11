#nullable enable
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class EnemySpawnerCounter : IUpdate
    {
        readonly EnemySpawner _enemySpawner;
        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;
        readonly WaveState _waveState;
        
        int CurrentSpawnedEnemyCount { get; set; }
        int MaxSpawnedEnemyCount => _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnEnemyCount();
        double Timer { get;set; }
        
        public EnemySpawnerCounter(
            EnemySpawner enemySpawner
            , IEnemyWaveParamContainer enemyWaveParamContainer
            , WaveState waveState
            )
        {
            _enemySpawner = enemySpawner;
            _enemyWaveParamContainer = enemyWaveParamContainer;
            _waveState = waveState;
        }

        void IUpdate.Update()
        {
            Timer += Time.deltaTime;
            if (Timer > _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnIntervalSec())
            {
                Timer = 0;
                if (CurrentSpawnedEnemyCount < MaxSpawnedEnemyCount)
                {
                    _enemySpawner.SpawnEnemy();
                    CurrentSpawnedEnemyCount++;
                }
            }
            if(CurrentSpawnedEnemyCount == MaxSpawnedEnemyCount)
            {
                CurrentSpawnedEnemyCount = 0;
                _waveState.NextWave();
            }
        }
    } 
}

