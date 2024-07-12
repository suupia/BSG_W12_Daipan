#nullable enable
using UnityEngine;

namespace Daipan.Enemy.Interfaces
{
    public interface IFinalBossViewParamData
    {
        // Colors
        Color GetBodyColor() => Color.white;
        Color GetEyeColor() => Color.white;
        Color GetEyeBallColor() => Color.white;
        Color GetLineColor() => Color.white; 
    } 
}

