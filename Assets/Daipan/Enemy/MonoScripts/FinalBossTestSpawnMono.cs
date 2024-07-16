#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.MonoScripts
{
    public class FinalBossTestSpawnMono : MonoBehaviour
    {
        FinalBossSpawner _finalBossSpawner = null!;

        [Inject]
        public void Init(FinalBossSpawner finalBossSpawner)
        {
            _finalBossSpawner = finalBossSpawner;
        }

        void Update()
        {
#if UNITY_EDITOR
            if(Input.GetKeyDown(KeyCode.B))
            {
                Debug.LogWarning("BOSS TEST SPAWNED");
                _finalBossSpawner.SpawnFinalBoss();
            }
#endif
        }
    }
}