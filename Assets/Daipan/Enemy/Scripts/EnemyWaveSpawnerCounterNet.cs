#nullable enable
using System;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Fusion;
using UnityEngine;
using R3;
using VContainer;

namespace Daipan.Enemy.Scripts
{
    public class EnemyWaveSpawnerCounterNet : NetworkBehaviour, IEnemyWaveSpawnerCounter, IDisposable
    {
        IEnemySpawner _enemySpawner = null!;
        IFinalBossSpawner _finalBossSpawner = null!;
        IEnemyWaveParamContainer _enemyWaveParamContainer = null!;
        WaveState _waveState;
        public int CurrentSpawnedEnemyCount { get; private set; }
        public int MaxSpawnedEnemyCount => _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnEnemyCount();
        double Timer { get; set; }
        bool IsInWaveInterval { get; set; }
        IDisposable? _waveSpawnDisposable;
        readonly CompositeDisposable _disposables = new();

        [Inject]
        public void Initialize(
            IEnemySpawner enemySpawner
            , IFinalBossSpawner finalBossSpawner
            , IEnemyWaveParamContainer enemyWaveParamContainer
            , WaveState waveState
        )
        {
            _enemySpawner = enemySpawner;
            _finalBossSpawner = finalBossSpawner;
            _enemyWaveParamContainer = enemyWaveParamContainer;
            _waveState = waveState;
        }

        public override void Spawned()
        {
            base.Spawned();
            _disposables.Add(
                Observable
                    .EveryValueChanged(_waveState, x => x.CurrentWaveIndex)
                    .Subscribe(_ => CurrentSpawnedEnemyCount = 0)
            );
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            Debug.Log($"IsInWaveInterval: {IsInWaveInterval} CurrentSpawnedEnemyCount: {CurrentSpawnedEnemyCount} MaxSpawnedEnemyCount: {MaxSpawnedEnemyCount}");

            IntervalSpawnEnemy();

            DelayNextWave();
        }

        void IntervalSpawnEnemy()
        {
            if (IsInWaveInterval) return;

            Timer += Time.deltaTime;
            if (Timer <= _enemyWaveParamContainer.GetEnemyWaveParamData().GetSpawnIntervalSec()) return;
            Timer = 0;

            if (CurrentSpawnedEnemyCount >= MaxSpawnedEnemyCount) return;

            // LastWaveの時はFinalBossをスポーン
            if (_waveState.CurrentWaveIndex == _enemyWaveParamContainer.WaveTotalCount - 1)
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

        void DelayNextWave()
        {
            if (IsInWaveInterval) return;

            if (CurrentSpawnedEnemyCount == MaxSpawnedEnemyCount)
            {
                IsInWaveInterval = true;
                _waveSpawnDisposable?.Dispose();
                _waveSpawnDisposable = Observable
                    .Timer(TimeSpan.FromSeconds(_enemyWaveParamContainer.GetEnemyWaveParamData().GetWaveIntervalSec())) // これを逐次評価したいがわからないので、IUpdateにしている
                    .Subscribe(_ =>
                    {
                        _waveState.NextWave();
                        IsInWaveInterval = false;
                    });
            }
        }

        // デバッグ用
        public void ResetCounter()
        {
            CurrentSpawnedEnemyCount = 0;
            Timer = 0;
            IsInWaveInterval = false;
        }

        public void Dispose()
        {
            _enemySpawner.Dispose();
            _waveSpawnDisposable?.Dispose();
            _disposables.Dispose();
        }

        void OnDestroy()
        {
            Dispose();
        }
    }
}