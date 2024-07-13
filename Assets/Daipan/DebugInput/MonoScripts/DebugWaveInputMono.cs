#nullable enable
using System;
using Daipan.Battle.scripts;
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
        EnemyWaveSpawnerCounter _enemyWaveSpawnerCounter = null!;
        IrritatedValue _irritatedValue = null!;

        [Inject]
        public void Initialize(
            WaveState waveState
            , EnemyWaveSpawnerCounter enemyWaveSpawnerCounter
            , IrritatedValue irritatedValue
        )
        {
            _waveState = waveState;
            _enemyWaveSpawnerCounter = enemyWaveSpawnerCounter;
            _irritatedValue = irritatedValue;
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
                _irritatedValue.IncreaseValue(_irritatedValue.MaxValue);
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

        static void SetNearLastTime(StreamTimer streamTimer, StreamData streamData)
        {
            var nearLastTime = streamData.GetMaxTime() - 3;
            Debug.Log($"SetNearLastTime nearLastTime: {nearLastTime}");
            streamTimer.SetTime(nearLastTime);
        }
    }
}