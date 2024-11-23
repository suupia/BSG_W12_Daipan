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
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;
        readonly SpawnEnemyCostValue _spawnEnemyCost;
        readonly IEnemySpawnedCostParam _spawnedCostParam;
        readonly AntiCommentObserver _antiCommentObserver;

        [Inject]
        public AntiEnemySpawnerNetwork(
            RpcReceiverNetWrapper streamerRPCReceiverNetWrapper
            , SpawnEnemyCostValue spawnEnemyCost
            , IEnemySpawnedCostParam enemySpawnedCostParam
            , AntiCommentObserver antiCommentObserver
        )
        {
            _rpcReceiverNetWrapper = streamerRPCReceiverNetWrapper;
            _spawnEnemyCost = spawnEnemyCost;
            _spawnedCostParam = enemySpawnedCostParam;
            _antiCommentObserver = antiCommentObserver;
        }

        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            if (enemyEnum == EnemyEnum.None) return;
            Debug.Log($"Anti spawns {enemyEnum}");
            Debug.Log($"Left Cost is {_spawnEnemyCost.Value}");

            if (!CanSpawnEnemy(enemyEnum)) return;

            _spawnEnemyCost.DecreaseValue(GetCost(enemyEnum));
            _rpcReceiverNetWrapper.RpcReceiverNet.SpawnEnemyRPC(enemyEnum);

            _antiCommentObserver.ResetCount();
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