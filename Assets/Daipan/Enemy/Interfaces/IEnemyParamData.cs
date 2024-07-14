#nullable enable
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamData
    {
        // Enum
        EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        int GetAttackAmount() => 1;
        double GetAttackIntervalSec() => 1;
        double GetAttackRange() => 2;
        public double GetIncreaseIrritatedValueOnAttack() => 10;

        // Hp
        int GetMaxHp() => 0;

        // Move
        double GetMoveSpeedPerSec() => 0;

    } 
}

