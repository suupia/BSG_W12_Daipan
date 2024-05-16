#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttack
    {
        public EnemyAttackParameter enemyAttackParameter = null!;

        readonly EnemyMono _enemyMono;
        float Timer { get; set; }

        public EnemyAttack(EnemyMono enemyMono)
        {
            _enemyMono = enemyMono;
        }
        public void Update(PlayerMono playerMono)
        {
            Timer += Time.deltaTime;
            if (Timer >= enemyAttackParameter.coolTimeSeconds)
            {
                Attack(playerMono);
                Timer = 0;
            }
        }

        void Attack(PlayerMono playerMono)
        {
            Debug.Log("Attack");
            if (!CanAttack(_enemyMono, playerMono)) return;
            playerMono.CurrentHp -= enemyAttackParameter.attackAmount;
        }
        
        bool CanAttack(EnemyMono enemyMono, PlayerMono playerMono)
        {
            if (playerMono.CurrentHp <= 0) return false;
            Debug.Log($"enemy.transform.position : {enemyMono.transform.position}, player.transform.position : {playerMono.transform.position}");
            Debug.Log($"distance : {(playerMono.transform.position - enemyMono.transform.position).magnitude}");
            if ((playerMono.transform.position - enemyMono.transform.position).magnitude > enemyAttackParameter.range) return false;
            return true;
        }
        
    }
}