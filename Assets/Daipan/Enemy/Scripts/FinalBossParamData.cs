#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossParamData : IFinalBossParamData, IFinalBossViewParamData
    {
        readonly FinalBossParam _finalBossParam;
        
        public FinalBossParamData(FinalBossParamManager finalBossParamManager)
        {
            _finalBossParam = finalBossParamManager.finalBossParam;
        }


        public EnemyEnum GetEnemyEnum() => EnemyEnum.None;

        // Attack
        public int GetAttackAmount() => _finalBossParam.enemyParam.enemyAttackParam.attackAmount;
        public double GetAttackIntervalSec() => _finalBossParam.enemyParam.enemyAttackParam.attackIntervalSec;
        public double GetAttackRange() => _finalBossParam.enemyParam.enemyAttackParam.attackRange;

        // Hp
        public int GetMaxHp() => _finalBossParam.enemyParam.enemyHpParam.maxHp;

        // Move
        public double GetMoveSpeedPerSec() => _finalBossParam.enemyParam.enemyMoveParam.moveSpeedPerSec;
        
        // FinalBoss
        public double GetSummonActionIntervalSec() => _finalBossParam.summonActionIntervalSec;
        public int GetSummonEnemyCount() => _finalBossParam.summonEnemyCount;
        public double GetSummonEnemyIntervalSec() => _finalBossParam.summonEnemyIntervalSec;
        
        // View
        public Color GetBodyColor() => _finalBossParam.enemyParam.enemyAnimatorParam.bodyColor;
        public Color GetEyeColor() => _finalBossParam.enemyParam.enemyAnimatorParam.eyeColor;
        public Color GetEyeBallColor() => _finalBossParam.enemyParam.enemyAnimatorParam.eyeBallColor;
        public Color GetLineColor() => _finalBossParam.enemyParam.enemyAnimatorParam.lineColor; 
        
     } 
}

