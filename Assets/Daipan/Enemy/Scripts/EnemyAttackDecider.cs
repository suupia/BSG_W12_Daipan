#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttackDecider
    {
        float Timer { get; set; }

        public void AttackUpdate(EnemyMono enemyMono, IEnemyParamData enemyParamData, PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            Timer += Time.deltaTime;
            if (Timer >= enemyParamData.GetAttackDelayDec())
            {
                Timer = 0;
                Attack(enemyMono, enemyParamData,  playerMono, enemyViewMono);
            }
        }

        static void Attack(EnemyMono enemyMono, IEnemyParamData enemyParamData, PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            if (!CanAttack(enemyMono,enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            EnemyAttackModule.Attack(playerMono,enemyParamData);
        }
        
        static bool CanAttack(EnemyMono enemyMono, IEnemyParamData enemyParamData,  PlayerMono playerMono)
        {
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.transform.position.x - playerMono.transform.position.x > enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
    }
}