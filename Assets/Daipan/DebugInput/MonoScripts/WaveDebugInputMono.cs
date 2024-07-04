#nullable enable
using System;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.DebugInput.MonoScripts
{
    public sealed class WaveDebugInputMono : MonoBehaviour
    {
        StreamTimer _streamTimer = null!;
        EnemyParamManager _enemyParamManager = null!; 
       
        [Inject]
        public void Initialize(StreamTimer streamTimer, EnemyParamManager enemyParamManager)
        {
            _streamTimer = streamTimer;
            _enemyParamManager = enemyParamManager;
        } 
        
        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKey(KeyCode.Alpha1))
            {
                SetTime(_streamTimer, _enemyParamManager, 0);
            }
            if (Input.GetKey(KeyCode.Alpha2))
            {
                SetTime(_streamTimer, _enemyParamManager, 1);
            }
            if (Input.GetKey(KeyCode.Alpha3))
            {
                SetTime(_streamTimer, _enemyParamManager, 2);
            }
            if (Input.GetKey(KeyCode.Alpha4))
            {
                SetTime(_streamTimer, _enemyParamManager, 3);
            }
            if (Input.GetKey(KeyCode.Alpha5))
            {
                SetTime(_streamTimer, _enemyParamManager, 4);
            }
#endif
        }
        
        static void SetTime(StreamTimer streamTimer, EnemyParamManager enemyParamManager, int index)
        {
            if (index < 0 || index >= enemyParamManager.enemyTimeLineParams.Count)
            {
                Debug.LogWarning($" index is out of range. index: {index}");
                return;
            }
            Debug.Log($"SetTime index: {index}");
            streamTimer.SetTime(enemyParamManager.enemyTimeLineParams[index].startTime);
        }

    }
}