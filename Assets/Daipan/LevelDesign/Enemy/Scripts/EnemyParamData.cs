#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamData
    {
        // Sprite
        public Func<UnityEngine.Sprite?> GetSprite { get; init; } = () => null;
        // Animator
        public Func<UnityEngine.RuntimeAnimatorController?> GetAnimator { get; init; } = () => null;
        
        // Enum
        public required Func<EnemyEnum> EnemyEnum { get; init; } 
        
        // Attack
        public Func<int> GetAttackAmount { get; init; } = () => 10;
        public Func<double> GetAttackDelayDec { get; init; } = () => 1.0;
        public Func<double> GetAttackRange { get; init; } = () => 5.0;
        
        // Hp
        public Func<int> GetCurrentHp { get; init; } = () => 100;
        
        // Move
        public Func<double> GetMoveSpeedPreSec { get; init; } = () => 1.0;
        
        // Spawn
        public Func<double> GetSpawnRatio { get; init; } = () => 1.0;
        
    }
}