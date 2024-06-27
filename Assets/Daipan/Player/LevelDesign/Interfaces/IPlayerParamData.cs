#nullable enable
using System;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.LevelDesign.Interfaces
{
    public interface IPlayerParamData
    {
        UnityEngine.RuntimeAnimatorController? GetAnimator();
        PlayerColor PlayerEnum();
        int GetAttack();
    }
}
