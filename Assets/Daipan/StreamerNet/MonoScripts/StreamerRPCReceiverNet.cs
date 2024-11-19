#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using VContainer;
using Daipan.Enemy.Interfaces;

namespace Daipan.StreamerNet.MonoScripts
{
    public class StreamerRPCReceiverNet : NetworkBehaviour
    {
        private IEnemySpawner _enemySpawner = null!;

        [Inject]
        public void Initialize(
            IEnemySpawner enemySpawner
        )
        {
            _enemySpawner = enemySpawner;

            Debug.Log("StreamerRPCReceiverNet is initialized");
        }
    }
}