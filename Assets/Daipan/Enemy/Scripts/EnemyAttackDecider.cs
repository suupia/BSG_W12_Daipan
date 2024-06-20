#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttackDecider
    {
        readonly EnemyParamWarp _enemyParamWarp;
        readonly EnemyAttack _enemyAttack;
        readonly EnemyMono _enemyMono;
        float Timer { get; set; }

        public EnemyAttackDecider(
            EnemyMono enemyMono,
            EnemyParamWarp enemyParamWarp,
            EnemyAttack enemyAttack
            )
        {
            _enemyParamWarp = enemyParamWarp;
            _enemyMono = enemyMono;
            _enemyAttack = enemyAttack;
        }
        public void AttackUpdate(PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            Timer += Time.deltaTime;
            if (Timer >= _enemyParamWarp.GetAttackDelayDec())
            {
                Attack(playerMono, enemyViewMono);
                Timer = 0;
            }
        }

        void Attack(PlayerMono playerMono, AbstractEnemyViewMono? enemyViewMono)
        {
            if (!CanAttack(_enemyMono, playerMono)) return;
            if (enemyViewMono != null) enemyViewMono.Attack();
            _enemyAttack.Attack(playerMono);
        }
        
        bool CanAttack(EnemyMono enemyMono, PlayerMono playerMono)
        {
            if (playerMono.CurrentHp <= 0) return false;
            Debug.Log($"enemy.transform.position : {enemyMono.transform.position}, player.transform.position : {playerMono.transform.position}");
            Debug.Log($"distance : {(playerMono.transform.position - enemyMono.transform.position).magnitude}");
            if (enemyMono.transform.position.x - playerMono.transform.position.x > _enemyParamWarp.GetAttackRange()) return false;
            return true;
        }
        
    }
}