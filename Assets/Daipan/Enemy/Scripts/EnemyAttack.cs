#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        readonly EnemyParamWarp _enemyParamWarp;
        public EnemyAttack(EnemyParamWarp enemyParamWarp)
        {
            _enemyParamWarp = enemyParamWarp;
        }
        
        public void Attack(IPlayerHp playerHp)
        {
            playerHp.SetHp(
                new DamageArgs(_enemyParamWarp.GetAttackAmount(),
                _enemyParamWarp.GetEnemyEnum())
                );
        }
    } 

}

