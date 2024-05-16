#nullable enable
using Daipan.Battle.interfaces;
using Daipan.Enemy.MonoScripts;
using UnityEngine;

namespace Daipan.Enemy.Scripts
{
    public sealed class EnemyAttack
    {
        public EnemyAttackParameter enemyAttackParameter = null!;

        EnemyMono? _enemyMono;
        float Timer { get; set; }
        public void SetEnemyMono(EnemyMono enemyMono)
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
            // [Prerequisite]
            if (_enemyMono == null)
            {
                Debug.LogWarning($"_enemyMono is null");
                return;
            }
            if (!CanAttack(_enemyMono, playerMono)) return;
            playerMono.CurrentHp -= enemyAttackParameter.attackAmount;
        }
        
        bool CanAttack(EnemyMono enemyMono, PlayerMono playerMono)
        {
            if (playerMono.CurrentHp <= 0) return false;
            if ((playerMono.transform.position - enemyMono.transform.position).magnitude > enemyAttackParameter.range) return false;
            return true;
        }
        
    }
}