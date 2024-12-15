#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
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
using Daipan.Battle.Scripts;
using Daipan.Battle.interfaces;

namespace Daipan.StreamerNet.MonoScripts
{
    public class RpcReceiverNet : NetworkBehaviour
    {
        private NetworkRunner _runner = null!;
        private PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper = null!;

        private IAntiEnemySpawnerNetwork _antiEnemySpawner = null!;
        private AntiDaipanExecutor _antiDaipanExecutor = null!;
        private AntiStateValue _antiStateValue = null!;
        private IIrritatedGaugeValue _irritatedGaugeValue = null!;
        private WaveState _waveState = null!;
        private IResultState _resultState = null!;
        private IViewerNumber _viewerNumber = null!;
        private FinalBossColorChangerNetwork _finalBossColorChangerNetwork = null!;
        List<PlayerRef> _showResultPlayerRefs = new();


        [Inject]
        public void Initialize(
            NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , IAntiEnemySpawnerNetwork antiEnemySpawner
            , AntiDaipanExecutor antiDaipanExecutor
            , AntiStateValue antiStateValue
            , IIrritatedGaugeValue irritatedGaugeValue
            , WaveState waveState
            , IResultState resultState
            , IViewerNumber viewerNumber
            , FinalBossColorChangerNetwork finalBossColorChangerNetwork
        )
        {
            _runner = runner;
            _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
            _antiEnemySpawner = antiEnemySpawner;
            _antiDaipanExecutor = antiDaipanExecutor;
            _antiStateValue = antiStateValue;
            _irritatedGaugeValue = irritatedGaugeValue;
            _waveState = waveState;
            _resultState = resultState;
            _viewerNumber = viewerNumber;
            _finalBossColorChangerNetwork = finalBossColorChangerNetwork;

            Debug.Log("StreamerRPCReceiverNet is initialized");
        }


        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SpawnAntiEnemyRPC(EnemyEnum enemyEnum, PlayerRef playerRef)
        {
            if (_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == PlayerRoleEnum.Streamer)
            {
                _antiEnemySpawner.SpawnAntiEnemy(enemyEnum, playerRef);
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

        [Rpc(RpcSources.All, RpcTargets.All)]
        public void ShowResultRPC(PlayerRoleEnum winPlayer)
        {
            _resultState.ShowResult(_playerDataTransporterNetWrapper.GetPlayerRoleEnum(_runner.LocalPlayer) == winPlayer);

            Debug.Log("Show Result RPC received");
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void AddTransitionScenePlayerRefRPC()
        {
            _showResultPlayerRefs.Add(_runner.LocalPlayer);
            if (_showResultPlayerRefs.Count == _runner.ActivePlayers.Count())
            {
                TransitionSceneRPC();
            }
            
        }

        [Rpc(RpcSources.All, RpcTargets.All)]
        void TransitionSceneRPC()
        {
            if(_runner.IsSharedModeMasterClient)
                 SceneTransition.TransitionSceneWithNetworkRunner(_runner, SceneName.ResultSceneNet);
        }


        
        
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SetViewerRPC(PlayerRef caller, int amount)
        {
            if (_runner.LocalPlayer == caller) return;
            _viewerNumber.SetViewer(amount);
        }
        
        [Rpc(RpcSources.All, RpcTargets.All)]
        public void SetFinalBossColorRPC(FinalBossColor color)
        {
            Debug.Log($"SetFinalBossColorRPC received: {color}");
            _finalBossColorChangerNetwork.CurrentColor = color;
        }
    }
}