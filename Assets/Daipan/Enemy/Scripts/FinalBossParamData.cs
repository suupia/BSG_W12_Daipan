#nullable enable
using System.Linq;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public class FinalBossParamData : IFinalBossParamData, IFinalBossViewParamData
    {
        readonly FinalBossParam _finalBossParam;
        readonly FinalBossColorChanger _finalBossColorChanger;
        
        public FinalBossParamData(
            FinalBossParamManager finalBossParamManager
            , FinalBossColorChanger finalBossColorChanger
            )
        {
            _finalBossParam = finalBossParamManager.finalBossParam;
            _finalBossColorChanger = finalBossColorChanger;
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
        public double GetDaipanHitDamagePercent() => _finalBossParam.daipanHitDamagePercent;
        public double GetKnockBackDistance() => _finalBossParam.knockBackDistance;
        public int GetCommentCount() => _finalBossParam.commentCount;
        
        // View
        public Color GetBodyColor() =>
            _finalBossParam.finalBossColorParams.First(x =>
                x.finalBossColor == _finalBossColorChanger.CurrentColor).bodyColor;
        public Color GetEyeColor() =>  _finalBossParam.finalBossColorParams.First(x =>
                x.finalBossColor == _finalBossColorChanger.CurrentColor).eyeColor;
        public Color GetEyeBallColor() => _finalBossParam.finalBossColorParams.First(x =>
            x.finalBossColor == _finalBossColorChanger.CurrentColor).eyeBallColor;
        public Color GetLineColor() => _finalBossParam.finalBossColorParams.First(x =>
            x.finalBossColor == _finalBossColorChanger.CurrentColor).lineColor;
        
     } 
}

