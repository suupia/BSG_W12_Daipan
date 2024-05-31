#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamData
    {
        public required Func<EnemyEnum> EnemyEnum { get; init; } 
        public Func<int> AttackAmount { get; init; } = () => 10;
        public Func<double> AttackDelayDec { get; init; } = () => 1.0;
        public Func<double> AttackRange { get; init; } = () => 5.0;
    }
}