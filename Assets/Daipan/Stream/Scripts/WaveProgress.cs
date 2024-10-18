#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Unity.VisualScripting;
using UnityEngine;
using VContainer.Unity;

namespace Daipan.Stream.Scripts
{
    public sealed class WaveProgress 
    {
        readonly WaveState _waveState;
        readonly IEnemyWaveSpawnerCounter _enemyWaveSpawnerCounter;
        public double CurrentProgressRatio
            => CurrentWaveProgressRatio / _waveState.TotalWaveCount +
               (double)_waveState.CurrentWaveIndex / _waveState.TotalWaveCount;

        double CurrentWaveProgressRatio => (double)_enemyWaveSpawnerCounter.CurrentSpawnedEnemyCount /
                                           _enemyWaveSpawnerCounter.MaxSpawnedEnemyCount; 
        public WaveProgress(
            WaveState waveState
            , IEnemyWaveSpawnerCounter enemyWaveSpawnerCounter
            )
        {
            _waveState = waveState;
            _enemyWaveSpawnerCounter = enemyWaveSpawnerCounter;
        }

    }
}