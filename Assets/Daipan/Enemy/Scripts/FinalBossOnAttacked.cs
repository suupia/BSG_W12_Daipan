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
    public sealed class FinalBossOnAttacked : IEnemyOnAttacked
    {
        FinalBossColor CurrentColor => _finalBossColorChanger.CurrentColor;
        readonly FinalBossColorChanger _finalBossColorChanger;

        public FinalBossOnAttacked(FinalBossColorChanger finalBossColorChanger)
        {
            _finalBossColorChanger = finalBossColorChanger;
        }

        public Hp OnAttacked(Hp hp, IPlayerParamData playerParamData)
        {
            Debug.Log($"OnAttacked hp: {hp.Value} playerParamData: {playerParamData}");
            var attackPlayer = playerParamData.PlayerEnum();
            if (IsSameColor(CurrentColor, attackPlayer))
                return new Hp(hp.Value - playerParamData.GetAttack());
            return hp;
        }

        public static bool IsSameColor(FinalBossColor finalBossColor, PlayerColor playerColor)
        {
            return finalBossColor switch
            {
                FinalBossColor.Red => playerColor == PlayerColor.Red,
                FinalBossColor.Blue => playerColor == PlayerColor.Blue,
                FinalBossColor.Yellow => playerColor == PlayerColor.Yellow,
                _ => false
            };
        }

    }
}