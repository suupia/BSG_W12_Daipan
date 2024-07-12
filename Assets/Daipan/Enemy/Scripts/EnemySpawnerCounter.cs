#nullable enable
using System;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using UnityEngine;
using R3;

namespace Daipan.Enemy.Scripts
{
    public class EnemySpawnerCounter : IUpdate, IDisposable
    {
        readonly EnemySpawner _enemySpawner;
        readonly FinalBossSpawner _finalBossSpawner;
        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;
        readonly WaveState _waveState;
        int CurrentSpawnedEnemyCount { get; set; }
        int MaxSpawnedEnemyCount => _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnEnemyCount();
        double Timer { get; set; }
        bool IsInWaveInterval { get; set; }
        IDisposable? _disposable;

        public EnemySpawnerCounter(
            EnemySpawner enemySpawner
            , FinalBossSpawner finalBossSpawner
            , IEnemyWaveParamContainer enemyWaveParamContainer
            , WaveState waveState
        )
        {
            _enemySpawner = enemySpawner;
            _finalBossSpawner = finalBossSpawner;
            _enemyWaveParamContainer = enemyWaveParamContainer;
            _waveState = waveState;
        }

        void IUpdate.Update()
        {
            IntervalSpawnEnemy();

            if (CurrentSpawnedEnemyCount == MaxSpawnedEnemyCount)
            {
                CurrentSpawnedEnemyCount = 0;
                IsInWaveInterval = true;
                _disposable?.Dispose();
                _disposable = Observable
                    .Timer(TimeSpan.FromSeconds(_enemyWaveParamContainer.GetEnemyWaveParamData().GetWaveIntervalSec()))
                    .Subscribe(_ =>
                    {
                        _waveState.NextWave();
                        IsInWaveInterval = false;
                    });
            }
        }

        void IntervalSpawnEnemy()
        {
            if (IsInWaveInterval) return;

            Timer += Time.deltaTime;
            if (Timer <= _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnIntervalSec()) return;
            Timer = 0;

            if (CurrentSpawnedEnemyCount >= MaxSpawnedEnemyCount) return;

            // LastWaveの時はFinalBossをスポーン
            if (_waveState.CurrentWave == _enemyWaveParamContainer.WaveTotalCount - 1)
            {
                _finalBossSpawner.SpawnFinalBoss();
                CurrentSpawnedEnemyCount++;
            }
            else
            {
                _enemySpawner.SpawnEnemy();
                CurrentSpawnedEnemyCount++;
            }
        }


        public void Dispose()
        {
            _enemySpawner.Dispose();
            _disposable?.Dispose();
        }

        ~EnemySpawnerCounter()
        {
            Dispose();
        }
    }
}