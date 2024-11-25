#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;
using VContainer;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Transporter;
using Daipan.AntiNet.Scripts;

namespace Daipan.StreamerNet.MonoScripts
{
    public class RpcReceiverNet : NetworkBehaviour
    {
        private NetworkRunner _runner = null!;
        private PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper = null!;

        private IEnemySpawner _enemySpawner = null!;
        private AntiDaipanExecutor _antiDaipanExecutor = null!;
        private AntiStateValue _antiStateValue = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , IEnemySpawner enemySpawner
            , AntiDaipanExecutor antiDaipanExecutor
            , AntiStateValue antiStateValue
        )
        {
            _runner = runner;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
            _enemySpawner = enemySpawner;
            _antiDaipanExecutor = antiDaipanExecutor;
            _antiStateValue = antiStateValue;

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

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void DaipanRPC()
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                _antiDaipanExecutor.DaiPan();
                Debug.Log("Daipan RPC received");
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SetFeverRPC()
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                _antiStateValue.SetFever();
                Debug.Log("Set Fever RPC received");
            }
        }

    }
}