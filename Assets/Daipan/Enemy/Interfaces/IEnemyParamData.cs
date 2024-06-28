#nullable enable
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamData
    {
        // Enum
        EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        int GetAttackAmount() => 0;
        double GetAttackDelayDec() => 0;
        double GetAttackRange() => 0;

        // Hp
        int GetCurrentHp() => 0;

        // Move
        double GetMoveSpeedPerSec() => 0;

        // Spawn
        double GetSpawnRatio() => 0;

        // Irritated value
        int GetIrritationAfterKill() => 0;

        // Colors
        Color GetBodyColor() => Color.white;
        Color GetEyeColor() => Color.white;
        Color GetEyeBallColor() => Color.white;
        Color GetLineColor() => Color.white;
    } 
}

