#nullable enable
using System;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyParamData
    {
        public Func<int> AttackAmount { get; init; } = () => 10;
        public Func<double> AttackDelayDec { get; init; } = () => 1.0;
        public Func<double> AttackRange { get; init; } = () => 5.0;
    }
}