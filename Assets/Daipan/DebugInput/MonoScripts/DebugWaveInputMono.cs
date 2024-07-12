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
        StreamTimer _streamTimer = null!;
        StreamData _streamData = null!;
        WaveState _waveState = null!;
        EnemyParamsManager _enemyParamsManager = null!;

        [Inject]
        public void Initialize(
            StreamTimer streamTimer
            , StreamData streamData
            , WaveState waveState
            , EnemyParamsManager enemyParamsManager
        )
        {
            _streamTimer = streamTimer;
            _streamData = streamData;
            _waveState = waveState;
            _enemyParamsManager = enemyParamsManager;
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

            if (Input.GetKeyDown(KeyCode.Alpha5))
            {
               //  SetTime(_streamTimer, _enemyParamsManager, 4);
            }
#endif
        }

        static void ForceNextWave(WaveState waveState, int index)
        {
            while (waveState.CurrentWave < index)
            {
                waveState.NextWave();
                if(waveState.CurrentWave > 100) break;
            }
            Debug.Log($"ForceNextWave waveState.CurrentWave: {waveState.CurrentWave}");
        }

        static void SetNearLastTime(StreamTimer streamTimer, StreamData streamData)
        {
            var nearLastTime = streamData.GetMaxTime() - 3;
            Debug.Log($"SetNearLastTime nearLastTime: {nearLastTime}");
            streamTimer.SetTime(nearLastTime);
        }
    }
}