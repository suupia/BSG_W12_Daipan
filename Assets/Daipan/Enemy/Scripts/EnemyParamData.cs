#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyParamData : IEnemyParamData, IEnemyViewParamData
    {
        readonly EnemyParam _enemyParam;
        readonly IEnemyWaveParamContainer _enemyWaveParamContainer;


        public EnemyParamData(
            EnemyParam enemyParam
            , IEnemyWaveParamContainer enemyWaveParamContainer
        )
        {
            _enemyParam = enemyParam;
            _enemyWaveParamContainer = enemyWaveParamContainer;
        }
        // Enum
        public EnemyEnum GetEnemyEnum() => _enemyParam.enemyEnum;

        // Attack
        public int GetAttackAmount() => _enemyParam.enemyAttackParam.attackAmount;
        public double GetAttackIntervalSec() => _enemyParam.enemyAttackParam.attackIntervalSec;
        public double GetAttackRange() => _enemyParam.enemyAttackParam.attackRange;
        public double GetIncreaseIrritatedValueOnAttack() => _enemyParam.enemyAttackParam.increaseIrritatedValueOnAttack;

        // Hp
        public int GetMaxHp() => _enemyParam.enemyHpParam.maxHp;

        // Move
        public double GetMoveSpeedPerSec() => _enemyParam.enemyMoveParam.moveSpeedPerSec * _enemyWaveParamContainer.GetEnemyWaveParamData().GetMoveSpeedRate();

        // Colors
        public Color GetBodyColor() => IsSpawnedByAnti 
            ? ColorAdjuster.GetDarkenedColor(_enemyParam.enemyAnimatorParam.bodyColor) 
            : _enemyParam.enemyAnimatorParam.bodyColor;

        public Color GetEyeColor() => IsSpawnedByAnti 
            ? ColorAdjuster.GetDarkenedColor(_enemyParam.enemyAnimatorParam.eyeColor) 
            : _enemyParam.enemyAnimatorParam.eyeColor;

        public Color GetEyeBallColor() => IsSpawnedByAnti 
            ? ColorAdjuster.GetDarkenedColor(_enemyParam.enemyAnimatorParam.eyeBallColor) 
            : _enemyParam.enemyAnimatorParam.eyeBallColor;

        public Color GetLineColor() => IsSpawnedByAnti 
            ? ColorAdjuster.GetDarkenedColor(_enemyParam.enemyAnimatorParam.lineColor) 
            : _enemyParam.enemyAnimatorParam.lineColor;

        public bool IsSpawnedByAnti { get; set; }
    }

    public static class ColorAdjuster
    {
        const float DarkenFactor = 0.8f; // Adjust this factor to control the level of darkening

        public static Color GetDarkenedColor(Color color)
        {
            return new Color(
                color.r * DarkenFactor,
                color.g * DarkenFactor,
                color.b * DarkenFactor,
                color.a // Keep alpha unchanged
            );
        }
    }
}