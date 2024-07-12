#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossParamData : IFinalBossParamData, IFinalBossViewParamData
    {
        readonly FinalBossParamManager _finalBossParamManager;
        
        public FinalBossParamData(FinalBossParamManager finalBossParamManager)
        {
            _finalBossParamManager = finalBossParamManager;
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
        public double GetSummonActionIntervalSec() => _finalBossParamManager.finalBossParam.summonActionIntervalSec;
        public int GetSummonEnemyCount() => _finalBossParamManager.finalBossParam.summonEnemyCount;
        public double GetSummonEnemyIntervalSec() => _finalBossParamManager.finalBossParam.summonEnemyIntervalSec;
     } 
}

