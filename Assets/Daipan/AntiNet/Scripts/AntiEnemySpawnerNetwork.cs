#nullable enable
using System;
using UnityEngine;
using Daipan.Enemy.Scripts;
using VContainer;
using Daipna.StreamerNet.Scripts;
using Daipan.LevelDesign.Net;
using Fusion;
using Daipan.Sound.MonoScripts;

namespace Daipan.AntiNet.Scripts
{
    public class AntiEnemySpawnerNetwork
    {
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;
        readonly SpawnEnemyCostValue _spawnEnemyCost;
        readonly IEnemySpawnedCostParam _spawnedCostParam;
        readonly AntiCommentObserver _antiCommentObserver;
        readonly AntiStateValue _antiStateValue;
        readonly NetworkRunner _runner;

        [Inject]
        public AntiEnemySpawnerNetwork(
            RpcReceiverNetWrapper streamerRPCReceiverNetWrapper
            , SpawnEnemyCostValue spawnEnemyCost
            , IEnemySpawnedCostParam enemySpawnedCostParam
            , AntiCommentObserver antiCommentObserver
            , AntiStateValue antiStateValue
            , NetworkRunner runner
        )
        {
            _rpcReceiverNetWrapper = streamerRPCReceiverNetWrapper;
            _spawnEnemyCost = spawnEnemyCost;
            _spawnedCostParam = enemySpawnedCostParam;
            _antiCommentObserver = antiCommentObserver;
            _antiStateValue = antiStateValue;
            _runner = runner;
        }

        public void SpawnEnemy(EnemyEnum enemyEnum)
        {

            if (enemyEnum == EnemyEnum.None) return;
            Debug.Log($"Anti spawns {enemyEnum}");
            Debug.Log($"Left Cost is {_spawnEnemyCost.Value}");

            if (!CanSpawnEnemy(enemyEnum))
            {
                if (AntiStateEnum.BAN == _antiStateValue.AntiStateEnum)
                {
                    SoundManager.Instance?.PlaySe(SeEnum.Cancel);
                }
                else
                {
                    SoundManager.Instance?.PlaySe(SeEnum.Cancel);  // 同じSEを使うことにする（独断）
                }
                return;
            }
            SoundManager.Instance?.PlaySe(SeEnum.SummonEnemy); 

            _spawnEnemyCost.DecreaseValue(GetCost(enemyEnum));
            _rpcReceiverNetWrapper.RpcReceiverNet.SpawnAntiEnemyRPC(enemyEnum, _runner.LocalPlayer);

            _antiCommentObserver.ResetCount();
        }

        bool CanSpawnEnemy(EnemyEnum enemyEnum)
        {
            return _spawnEnemyCost.Value >= GetCost(enemyEnum);
        }

        int GetCost(EnemyEnum enemyEnum)
        {
            int multiplier = 1;
            if (_antiStateValue.AntiStateEnum == AntiStateEnum.BAN) multiplier *= 2;

            if (enemyEnum.IsBoss() == true) return _spawnedCostParam.BossEnemyCost * multiplier;
            return _spawnedCostParam.NormalEnemyCost * multiplier;
        }
    }
}