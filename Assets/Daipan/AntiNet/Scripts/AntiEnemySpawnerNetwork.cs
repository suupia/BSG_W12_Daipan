#nullable enable
using System;
using UnityEngine;
using Daipan.Enemy.Scripts;
using VContainer;
using Daipna.StreamerNet.Scripts;

namespace Daipan.AntiNet.Scripts
{
    public class AntiEnemySpawnerNetwork
    {
        readonly StreamerRPCReceiverNetWrapper _streamerRPCReceiverNetWrapper;
        [Inject]
        public AntiEnemySpawnerNetwork(
            StreamerRPCReceiverNetWrapper streamerRPCReceiverNetWrapper
        )
        {
            _streamerRPCReceiverNetWrapper = streamerRPCReceiverNetWrapper;
        }

        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            Debug.Log($"Anti spawns {enemyEnum}");
            _streamerRPCReceiverNetWrapper.RPCReceiverNet.SpawnEnemyRPC(enemyEnum);
        }
    }
}