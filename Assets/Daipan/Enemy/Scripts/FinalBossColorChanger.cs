#nullable enable
using System;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossColorChanger
    {
        const double ChangeColorSec = 2f;
        public FinalBossColor CurrentColor { get; private set; }
        readonly CompositeDisposable _disposable = new();

        public FinalBossColorChanger()
        {
            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(ChangeColorSec))
                    .Subscribe(_ => CurrentColor = NextColor(CurrentColor))
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
    
    public enum FinalBossColor
    {
        Red,
        Blue,
        Yellow
    }
}