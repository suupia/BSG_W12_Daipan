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
        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;
        readonly WaveState _waveState;
        int CurrentSpawnedEnemyCount { get; set; }
        int MaxSpawnedEnemyCount => _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnEnemyCount();
        double Timer { get; set; }
        bool IsInWaveInterval { get; set; }
        IDisposable? _disposable;

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
            if (!IsInWaveInterval)
            {
                Timer += Time.deltaTime;
                Debug.Log($"WaveState: {_waveState.CurrentWave}");
                if (Timer > _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnIntervalSec()) // todo:ここでエラーが起きている
                {
                    Timer = 0;
                    if (CurrentSpawnedEnemyCount < MaxSpawnedEnemyCount)
                    {
                        _enemySpawner.SpawnEnemy();
                        CurrentSpawnedEnemyCount++;
                    }
                }
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