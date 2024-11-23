#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using VContainer;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Transporter;

namespace Daipan.StreamerNet.MonoScripts
{
    public class RpcReceiverNet : NetworkBehaviour
    {
        private NetworkRunner _runner = null!;
        private PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper = null!;

        private IEnemySpawner _enemySpawner = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , IEnemySpawner enemySpawner
        )
        {
            _runner = runner;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
            _enemySpawner = enemySpawner;

            Debug.Log("StreamerRPCReceiverNet is initialized");
        }


        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SpawnEnemyRPC(EnemyEnum enemyEnum)
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Streamer)
            {
                _enemySpawner.SpawnEnemy(enemyEnum);
                Debug.Log($"SpawnEnemy RPC received: {enemyEnum}");
            }
        }

    }
}