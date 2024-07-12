#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{

    public sealed class FinalBossSummonEnemyDecider
    {
        float Timer { get; set; }
        readonly FinalBossParam _finalBossParam = null!;
        
        /// <summary>
        /// Please call this method in Update method of MonoBehaviour
        /// </summary>
        public void AttackUpdate(
            EnemyMono enemyMono
            , AbstractFinalBossViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , PlayerMono playerMono
            )
        {
            Timer += Time.deltaTime;
            if (Timer >=_finalBossParam.summonEnemyIntervalSec)
            {
                Timer = 0;
                SummonEnemy(enemyMono,enemyViewMono,enemyParamData, playerMono);
            }

        }

        static void SummonEnemy(
            EnemyMono enemyMono
            , AbstractFinalBossViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , PlayerMono playerMono
            )
        {
            if (!CanSummon(enemyMono,enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.SummonEnemy();
             // EnemyAttackModule.Attack(playerMono,enemyParamData);
             // todo: summon
        }
        
        static bool CanSummon(EnemyMono enemyMono, IEnemyParamData enemyParamData,  PlayerMono playerMono)
        {
            // todo : 実装する
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.transform.position.x - playerMono.transform.position.x > enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
    }
}