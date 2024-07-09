#nullable enable
using System;
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
        EnemyParamsManager _enemyParamsManager = null!; 
       
        [Inject]
        public void Initialize(StreamTimer streamTimer, EnemyParamsManager enemyParamsManager)
        {
            _streamTimer = streamTimer;
            _enemyParamsManager = enemyParamsManager;
        } 
        
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Alpha1))
            {
                SetTime(_streamTimer, _enemyParamsManager, 0);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                SetTime(_streamTimer, _enemyParamsManager, 1);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                SetTime(_streamTimer, _enemyParamsManager, 2);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                SetTime(_streamTimer, _enemyParamsManager, 3);
            }
            if (Input.GetKey(KeyCode.Alpha5))
            {
                SetTime(_streamTimer, _enemyParamsManager, 4);
            }
#endif
        }
        
        static void SetTime(StreamTimer streamTimer, EnemyParamsManager enemyParamsManager, int index)
        {
            if (index < 0 || index >= enemyParamsManager.enemyTimeLineParams.Count)
            {
                Debug.LogWarning($" index is out of range. index: {index}");
                return;
            }
            Debug.Log($"SetTime index: {index}");
            streamTimer.SetTime(enemyParamsManager.enemyTimeLineParams[index].startTime);
        }

    }
}