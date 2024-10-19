#nullable enable
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttackDecider
    {
        float Timer { get; set; }
        
        /// <summary>
        /// Please call this method in Update method of MonoBehaviour
        /// </summary>
        public void AttackUpdate(
            IEnemyMono enemyMono
            , IEnemyViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , IPlayerMono playerMono
            )
        {
            Timer += Time.deltaTime;
            if (Timer >= enemyParamData.GetAttackIntervalSec())
            {
                Timer = 0;
                Attack(enemyMono,enemyViewMono,enemyParamData, playerMono);
            }
        }

        static void Attack(
            IEnemyMono enemyMono
            , IEnemyViewMono? enemyViewMono
            , IEnemyParamData enemyParamData
            , IPlayerMono playerMono
            )
        {
            if (!CanAttack(enemyMono,enemyParamData, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            EnemyAttackModule.Attack(playerMono,enemyParamData);
        }
        
        static bool CanAttack(IEnemyMono enemyMono, IEnemyParamData enemyParamData, IPlayerMono playerMono)
        {
            if (playerMono.Hp.Value <= 0) return false;
            if (enemyMono.Transform.position.x - playerMono.Transform.position.x > enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
    }
}