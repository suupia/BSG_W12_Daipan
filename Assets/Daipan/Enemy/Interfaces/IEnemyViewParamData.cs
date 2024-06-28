#nullable enable
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyViewParamData
    {
        // Enum
        EnemyEnum GetEnemyEnum() => EnemyEnum.None;
        // Colors
        Color GetBodyColor() => Color.white;
        Color GetEyeColor() => Color.white;
        Color GetEyeBallColor() => Color.white;
        Color GetLineColor() => Color.white;
    } 
}

