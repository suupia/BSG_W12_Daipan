#nullable enable
using Daipan.Battle.interfaces;
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


        public PlayerHpNew AttackUpdate(EnemyMono enemyMono,IEnemyParamData _enemyParamData, PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            Timer += Time.deltaTime;
            if (Timer >= _enemyParamData.GetAttackDelayDec())
            {
                Timer = 0;
                return Attack(enemyMono, _enemyParamData,  playerMono, enemyViewMono);
            }
            return playerMono.PlayerHpNew;
        }

        static PlayerHpNew  Attack(EnemyMono _enemyMono,IEnemyParamData _enemyParamData, PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            if (!CanAttack(_enemyMono,_enemyParamData, playerMono)) return playerMono.PlayerHpNew;
            if (enemyViewMono != null) enemyViewMono.Attack();
            return EnemyAttackNew.Attack(_enemyParamData,playerMono.PlayerHpNew);
        }
        
        static bool CanAttack(EnemyMono enemyMono,IEnemyParamData _enemyParamData,  PlayerMono playerMono)
        {
            if (playerMono.CurrentHp <= 0) return false;
            if (enemyMono.transform.position.x - playerMono.transform.position.x > _enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
    }
}