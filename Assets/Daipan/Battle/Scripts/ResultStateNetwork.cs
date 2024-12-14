#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.interfaces;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.Result.MonoScripts;
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using Daipan.Transporter;
using Daipna.StreamerNet.Scripts;
using Fusion;
using R3;
using UnityEngine;
using VContainer;

namespace Daipan.Battle.Scripts
{
    public class ResultStateNetwork : IDisposable, IResultState
    {
        public ResultEnum CurrentResultEnum { get; private set; } = ResultEnum.None;
        readonly ResultViewMono _resultViewMono;
        readonly List<IDisposable> _disposables = new();
        readonly NetworkRunner _runner;
        readonly PlayerRoleEnum _localPlayerRoleEnum;
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;

        [Inject]
        public ResultStateNetwork(
            WaveProgress waveProgress
            , ResultViewMono resultViewMono
            , FinalBossDefeatTracker finalBossDefeatTracker
            , NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
            , IViewerNumber viewerNumber
        )
        {
            _resultViewMono = resultViewMono;
            _runner = runner;


            if (playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) != PlayerRoleEnum.Streamer) return;
            _disposables.Add(Observable.EveryUpdate()
                .Where(_ => CurrentResultEnum == ResultEnum.None)
                .Subscribe(_ =>
                {
                    if (waveProgress.CurrentProgressRatio >= 1 && finalBossDefeatTracker.IsFinalBossDefeated &&
                        CurrentResultEnum == ResultEnum.None)
                    {
                        const double delaySec = 2;
                        _disposables.Add(
                            Observable
                                .Timer(TimeSpan.FromSeconds(delaySec))
                                .Subscribe(_ => rpcReceiverNetWrapper.RpcReceiverNet.ShowResultRPC(PlayerRoleEnum.Streamer))
                        );
                    }
                }));
            _disposables.Add(Observable.EveryValueChanged(viewerNumber, x => x.Number)
                .Subscribe(_ =>
                {
                    if (viewerNumber.Number <= 0) rpcReceiverNetWrapper.RpcReceiverNet.ShowResultRPC(PlayerRoleEnum.Anti);
                }));
            _localPlayerRoleEnum = playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer);
            _rpcReceiverNetWrapper = rpcReceiverNetWrapper;
        }

        public void ShowResult(bool isClear)
        {
            Debug.Log($"_localPlayerRoleEnum:{_localPlayerRoleEnum}, isClear:{isClear}, IsSharedModeMasterClient:{_runner.IsSharedModeMasterClient}");
            NetworkPlayerResultHolder.NetworkPlayerResultEnum = isClear
                ? _localPlayerRoleEnum switch
                {
                    PlayerRoleEnum.Streamer => NetworkPlayerResultEnum.StreamerWin,
                    PlayerRoleEnum.Anti => NetworkPlayerResultEnum.AntiWin,
                    _ => NetworkPlayerResultEnum.StreamerWin // フェールセーフ 
                }
                : _localPlayerRoleEnum switch
                {
                    PlayerRoleEnum.Streamer => NetworkPlayerResultEnum.AntiWin,
                    PlayerRoleEnum.Anti => NetworkPlayerResultEnum.StreamerWin,
                    _ => NetworkPlayerResultEnum.StreamerWin // フェールセーフ
                };

            _rpcReceiverNetWrapper.RpcReceiverNet.AddTransitionScenePlayerRefRPC();
        }

        public void ShowDetails()
        {
            CurrentResultEnum = ResultEnum.Details;
            Debug.Log($"ShowDetails CurrentResultEnum:{CurrentResultEnum}");
            _resultViewMono.ShowDetails();
        }

        public void Dispose()
        {
            foreach (var disposable in _disposables) disposable.Dispose();
        }

        ~ResultStateNetwork()
        {
            Dispose();
        }
    }
}