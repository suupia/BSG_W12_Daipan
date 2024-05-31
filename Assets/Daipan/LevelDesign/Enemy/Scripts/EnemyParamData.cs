#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamData
    {
        // Enum
        public required Func<EnemyEnum> EnemyEnum { get; init; } 
        
        // Attack
        public Func<int> AttackAmount { get; init; } = () => 10;
        public Func<double> AttackDelayDec { get; init; } = () => 1.0;
        public Func<double> AttackRange { get; init; } = () => 5.0;
        
        // Hp
        public Func<int> GetCurrentHp { get; init; } = () => 100;
        
    }
}