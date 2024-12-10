#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.StreamerNet.MonoScripts;
using Daipna.StreamerNet.Scripts;
using Fusion;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossColorChangerNetwork : IFinalBossCurrentColor, IDisposable
    {
        const double ChangeColorSec = 2f;
        public FinalBossColor CurrentColor { get; set; }
        readonly CompositeDisposable _disposable = new();

        public FinalBossColorChangerNetwork(NetworkRunner runner, RpcReceiverNetWrapper rpcReceiverNetWrapper)
        {
            if (!runner.IsSharedModeMasterClient) return;
            
            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(ChangeColorSec))
                    .Subscribe(_ => rpcReceiverNetWrapper.RpcReceiverNet.SetFinalBossColorRPC(NextColor(CurrentColor)))
                );
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        static FinalBossColor NextColor(FinalBossColor currentColor)
        {
            return currentColor switch
            {
                FinalBossColor.Red => FinalBossColor.Blue,
                FinalBossColor.Blue => FinalBossColor.Yellow,
                FinalBossColor.Yellow => FinalBossColor.Red,
                _ => throw new ArgumentOutOfRangeException(nameof(currentColor), currentColor, null)
            };
        }
    }
}