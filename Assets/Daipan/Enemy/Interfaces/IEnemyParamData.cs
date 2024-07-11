#nullable enable
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamData
    {
        // Enum
        EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        int GetAttackAmount() => 0;
        double GetAttackIntervalSec() => 0;
        double GetAttackRange() => 0;

        // Hp
        int GetMaxHp() => 0;

        // Move
        double GetMoveSpeedPerSec() => 0;

    } 
}

