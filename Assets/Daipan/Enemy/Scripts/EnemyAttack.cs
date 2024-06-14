#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        readonly EnemyParamData _enemyParamData;
        public EnemyAttack(EnemyParamData enemyParamData)
        {
            _enemyParamData = enemyParamData;
        }
        
        public void Attack(IPlayerHp playerHp)
        {
            playerHp.SetHp(new DamageArgs(_enemyParamData.GetAttackAmount()));
            var playerMono = (PlayerMono)playerHp;
            playerMono.OnAttacked(_enemyParamData.GetEnemyEnum());
        }
    } 

}

