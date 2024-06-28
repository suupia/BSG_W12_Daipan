#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        readonly IEnemyParamData _enemyParamData;
        public EnemyAttack(IEnemyParamData enemyParamData)
        {
            _enemyParamData = enemyParamData;
        }
        
        public void Attack(IPlayerHp playerHp)
        {
            playerHp.SetHp(
                new DamageArgs(_enemyParamData.GetAttackAmount(),
                _enemyParamData.GetEnemyEnum())
                );
        }
    } 

}

