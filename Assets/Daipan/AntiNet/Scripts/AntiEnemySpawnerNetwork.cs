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
        // todo 値出し
        readonly int NormalCost = 1;
        readonly int BossCost = 5;
        //
        readonly StreamerRPCReceiverNetWrapper _streamerRPCReceiverNetWrapper;
        private SpawnEnemyCostValue _spawnEnemyCost;
        [Inject]
        public AntiEnemySpawnerNetwork(
            StreamerRPCReceiverNetWrapper streamerRPCReceiverNetWrapper
            , SpawnEnemyCostValue spawnEnemyCost
        )
        {
            _streamerRPCReceiverNetWrapper = streamerRPCReceiverNetWrapper;
            _spawnEnemyCost = spawnEnemyCost;
        }

        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            if (enemyEnum == EnemyEnum.None) return;
            Debug.Log($"Anti spawns {enemyEnum}");
            Debug.Log($"Left Cost is {_spawnEnemyCost.Value}");

            if (!CanSpawnEnemy(enemyEnum)) return;

            _spawnEnemyCost.DecreaseValue(GetCost(enemyEnum));
            _streamerRPCReceiverNetWrapper.RPCReceiverNet.SpawnEnemyRPC(enemyEnum);
        }

        bool CanSpawnEnemy(EnemyEnum enemyEnum)
        {
            return _spawnEnemyCost.Value >= GetCost(enemyEnum);
        }

        int GetCost(EnemyEnum enemyEnum)
        {
            if (enemyEnum.IsBoss() == true) return BossCost;
            return NormalCost;
        }
    }
}