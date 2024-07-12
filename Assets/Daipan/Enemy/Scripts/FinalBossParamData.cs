#nullable enable
using Daipan.Enemy.Interfaces;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossParamData : IFinalBossParamData, IFinalBossViewParamData 
    {
        readonly FinalBossParam _finalBossParam;
        
        public FinalBossParamData(FinalBossParam finalBossParam)
        {
            _finalBossParam = finalBossParam;
        }

        public double GetSummonActionIntervalSec() => 1;
        public int GetSummonEnemyCount() => 5;
        public double GetSummonEnemyIntervalSec() => 1;
        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        public int GetAttackAmount() => 0;
        public double GetAttackIntervalSec() => 0;
        public double GetAttackRange() => 0;

        // Hp
        public int GetMaxHp() => 0;

        // Move
        public double GetMoveSpeedPerSec() => 0;
    } 
}

