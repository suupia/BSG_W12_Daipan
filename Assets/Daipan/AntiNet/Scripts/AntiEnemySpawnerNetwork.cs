#nullable enable
using System;
using UnityEngine;
using Daipan.Enemy.Scripts;
using VContainer;
using Daipna.StreamerNet.Scripts;
using Daipan.LevelDesign.Net;

namespace Daipan.AntiNet.Scripts
{
    public class AntiEnemySpawnerNetwork
    {
        readonly StreamerRPCReceiverNetWrapper _streamerRPCReceiverNetWrapper;
        readonly SpawnEnemyCostValue _spawnEnemyCost;
        readonly IEnemySpawnedCostParam _spawnedCostParam;

        [Inject]
        public AntiEnemySpawnerNetwork(
            StreamerRPCReceiverNetWrapper streamerRPCReceiverNetWrapper
            , SpawnEnemyCostValue spawnEnemyCost
            , IEnemySpawnedCostParam enemySpawnedCostParam
        )
        {
            _streamerRPCReceiverNetWrapper = streamerRPCReceiverNetWrapper;
            _spawnEnemyCost = spawnEnemyCost;
            _spawnedCostParam = enemySpawnedCostParam;
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
            if (enemyEnum.IsBoss() == true) return _spawnedCostParam.BossEnemyCost;
            return _spawnedCostParam.NormalEnemyCost;
        }
    }
}