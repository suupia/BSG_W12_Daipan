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
using Daipan.Stream.Scripts;
using Daipan.Stream.Interfaces;
using Daipan.Battle.scripts;

namespace Daipan.StreamerNet.MonoScripts
{
    public class RpcReceiverNet : NetworkBehaviour
    {
        private NetworkRunner _runner = null!;
        private PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper = null!;

        private IEnemySpawnerNetwork _enemySpawner = null!;
        private AntiDaipanExecutor _antiDaipanExecutor = null!;
        private AntiStateValue _antiStateValue = null!;
        private IIrritatedGaugeValue _irritatedGaugeValue = null!;
        private WaveState _waveState = null!;

        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , IEnemySpawnerNetwork enemySpawner
            , AntiDaipanExecutor antiDaipanExecutor
            , AntiStateValue antiStateValue
            , IIrritatedGaugeValue irritatedGaugeValue
            , WaveState waveState
        )
        {
            _runner = runner;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
            _enemySpawner = enemySpawner;
            _antiDaipanExecutor = antiDaipanExecutor;
            _antiStateValue = antiStateValue;
            _irritatedGaugeValue = irritatedGaugeValue;
            _waveState = waveState;

            Debug.Log("StreamerRPCReceiverNet is initialized");
        }


        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SpawnEnemyRPC(EnemyEnum enemyEnum, PlayerRef playerRef)
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Streamer)
            {
                _enemySpawner.SpawnEnemy(enemyEnum, playerRef);
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
        public void SetFeverRPC(PlayerRef specialPlayer)
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                bool isSpecial = specialPlayer == _runner.LocalPlayer;
                _antiStateValue.SetFever(isSpecial);
                Debug.Log("Set Fever RPC received");
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SetIrritatedValueRPC(double amount)
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                _irritatedGaugeValue.SetValue(amount);
                Debug.Log("Set IrritatedValue RPC received");
            }
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void NextWaveRPC()
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Anti)
            {
                _waveState.NextWave();
                Debug.Log("Set NextWave RPC received");
            }
        }

    }
}