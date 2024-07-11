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
        readonly IEnemyWaveParamContainer _enemyTimeLInePramContainer;


        public EnemyParamData(
            EnemyParam enemyParam
            , IEnemyWaveParamContainer enemyTimeLInePramContainer
        )
        {
            _enemyParam = enemyParam;
            _enemyTimeLInePramContainer = enemyTimeLInePramContainer;
        }
        // Enum

        public EnemyEnum GetEnemyEnum() => _enemyParam.enemyEnum;

        // Attack
        public int GetAttackAmount() => _enemyParam.enemyAttackParam.attackAmount;
        public double GetAttackDelayDec() => _enemyParam.enemyAttackParam.attackDelaySec;
        public double GetAttackRange() => _enemyParam.enemyAttackParam.attackRange;

        // Hp
        public int GetMaxHp() => _enemyParam.enemyHpParam.maxHp;
        public int GetCurrentHp() => _enemyParam.enemyHpParam.hpAmount;

        // Move
        public double GetMoveSpeedPerSec() => _enemyParam.enemyMoveParam.moveSpeedPerSec * _enemyTimeLInePramContainer.GetEnemyTimeLineParamData().GetMoveSpeedRate();

        // Spawn
        public double GetSpawnRatio() => _enemyParam.enemySpawnParam.spawnRatio;

        // Irritated value
        public int GetIrritationAfterKill() => _enemyParam.enemyRewardParam.irritationAfterKill;

        // Colors
        public Color GetBodyColor() => _enemyParam.enemyAnimatorParam.bodyColor;
        public Color GetEyeColor() => _enemyParam.enemyAnimatorParam.eyeColor;
        public Color GetEyeBallColor() => _enemyParam.enemyAnimatorParam.eyeBallColor;
        public Color GetLineColor() => _enemyParam.enemyAnimatorParam.lineColor;
    }
}