#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossParamData : IFinalBossParamData, IFinalBossViewParamData 
    {
        readonly FinalBossParam _finalBossParam;
        
        public FinalBossParamData(FinalBossParam finalBossParam)
        {
            _finalBossParam = finalBossParam;
        }


        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        public int GetAttackAmount() => 0;
        public double GetAttackIntervalSec() => 0;
        public double GetAttackRange() => 0;

        // Hp
        public int GetMaxHp() => 0;

        // Move
        public double GetMoveSpeedPerSec() => 0;
        
        // FinalBoss
        public double GetSummonActionIntervalSec() => _finalBossParam.summonActionIntervalSec;
        public int GetSummonEnemyCount() => _finalBossParam.summonEnemyCount;
        public double GetSummonEnemyIntervalSec() => _finalBossParam.summonEnemyIntervalSec;
     } 
}

