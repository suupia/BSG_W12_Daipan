#nullable enable
using System;
using Daipan.Battle.scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.DebugInput.MonoScripts
{
    public sealed class DebugWaveInputMono : MonoBehaviour
    {
        WaveState _waveState = null!;
        IEnemyWaveSpawnerCounter _enemyWaveSpawnerCounter = null!;
        IrritatedGaugeValueNetwork _irritatedGaugeValueNetwork = null!;

        [Inject]
        public void Initialize(
            WaveState waveState
            , IEnemyWaveSpawnerCounter enemyWaveSpawnerCounter
            , IrritatedGaugeValueNetwork irritatedGaugeValueNetwork
        )
        {
            _waveState = waveState;
            _enemyWaveSpawnerCounter = enemyWaveSpawnerCounter;
            _irritatedGaugeValueNetwork = irritatedGaugeValueNetwork;
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ForceNextWave(_waveState, 0); 
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ForceNextWave (_waveState, 1);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ForceNextWave(_waveState, 2); 
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                ForceNextWave(_waveState, 3);  
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                _irritatedGaugeValueNetwork.IncreaseValue(_irritatedGaugeValueNetwork.MaxValue);
            } 
#endif
        }

        void ForceNextWave(WaveState waveState, int index)
        {
            while (waveState.CurrentWaveIndex < index)
            {
                waveState.NextWave();
                if(waveState.CurrentWaveIndex > 100) break;
            }
            _enemyWaveSpawnerCounter.ResetCounter();
            Debug.Log($"ForceNextWave waveState.CurrentWave: {waveState.CurrentWaveIndex}");
        }

    }
}