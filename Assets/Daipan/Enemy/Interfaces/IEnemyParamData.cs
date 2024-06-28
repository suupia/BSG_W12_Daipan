#nullable enable
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamData
    {
        // Enum
        EnemyEnum GetEnemyEnum();

        // Attack
        int GetAttackAmount();
        double GetAttackDelayDec();
        double GetAttackRange();

        // Hp
        int GetCurrentHp();

        // Move
        double GetMoveSpeedPerSec();

        // Spawn
        double GetSpawnRatio();

        // Irritated value
        int GetIrritationAfterKill();

        // Colors
        Color GetBodyColor();
        Color GetEyeColor();
        Color GetEyeBallColor();
        Color GetLineColor(); 
    } 
}

