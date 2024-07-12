#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using R3;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class FinalBossOnAttacked : IEnemyOnAttacked, IDisposable
    {
        const double ChangeColorSec = 2f;
        FinalBossColor CurrentColor { get; set; }
        readonly CompositeDisposable _disposable = new();

        public FinalBossOnAttacked()
        {
            _disposable.Add(
                Observable
                    .Interval(TimeSpan.FromSeconds(ChangeColorSec))
                    .Subscribe(_ => CurrentColor = NextColor(CurrentColor))
            );
        }

        enum FinalBossColor
        {
            Red,
            Blue,
            Yellow
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }

        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            Debug.Log($"OnAttacked hp: {hp.Value} playerParamData: {playerParamData}");
            var attackPlayer = playerParamData.PlayerEnum();
            if (attackPlayer == PlayerColor.Red && CurrentColor == FinalBossColor.Red
                || attackPlayer == PlayerColor.Blue && CurrentColor == FinalBossColor.Blue
                || attackPlayer == PlayerColor.Yellow && CurrentColor == FinalBossColor.Yellow)
                return new Hp(hp.Value - playerParamData.GetAttack());
            return hp;
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