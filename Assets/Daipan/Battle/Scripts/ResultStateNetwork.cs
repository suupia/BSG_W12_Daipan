#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.interfaces;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.Stream.Scripts;
using Daipan.StreamerNet.MonoScripts;
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
        readonly ResultViewMono _resultViewMono;
        readonly List<IDisposable> _disposables = new();

        public ResultEnum CurrentResultEnum { get; private set; } = ResultEnum.None;

        [Inject]
        public ResultStateNetwork(
            WaveProgress waveProgress
            , ResultViewMono resultViewMono
            , FinalBossDefeatTracker finalBossDefeatTracker
            , NetworkRunner runner
            , PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
        )
        {
            _resultViewMono = resultViewMono;

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
        }

        public void ShowResult(bool isClear)
        {
            Time.timeScale = 0;
            _resultViewMono.ShowResult(isClear, () => CurrentResultEnum = ResultEnum.Result);
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