#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttackDecider
    {
        readonly EnemyParamData _enemyParamData;
        readonly EnemyAttack _enemyAttack;
        readonly EnemyMono _enemyMono;
        float Timer { get; set; }

        public EnemyAttackDecider(
            EnemyMono enemyMono,
            EnemyParamData enemyParamData,
            EnemyAttack enemyAttack
            )
        {
            _enemyParamData = enemyParamData;
            _enemyMono = enemyMono;
            _enemyAttack = enemyAttack;
        }
        public void AttackUpdate(PlayerMono playerMono)
        {
            Timer += Time.deltaTime;
            if (Timer >= _enemyParamData.GetAttackDelayDec())
            {
                Attack(playerMono);
                Timer = 0;
            }
        }

        void Attack(PlayerMono playerMono)
        {
            if (!CanAttack(_enemyMono, playerMono)) return;
            _enemyAttack.Attack(playerMono);
        }
        
        bool CanAttack(EnemyMono enemyMono, PlayerMono playerMono)
        {
            if (playerMono.CurrentHp <= 0) return false;
            Debug.Log($"enemy.transform.position : {enemyMono.transform.position}, player.transform.position : {playerMono.transform.position}");
            Debug.Log($"distance : {(playerMono.transform.position - enemyMono.transform.position).magnitude}");
            if (enemyMono.transform.position.x - playerMono.transform.position.x > _enemyParamData.GetAttackRange()) return false;
            return true;
        }
        
    }
}