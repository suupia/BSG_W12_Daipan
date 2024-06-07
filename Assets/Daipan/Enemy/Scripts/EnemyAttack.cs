#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttack
    {
        public event EventHandler<PlayerOnAttackedEventArgs>? PlayerOnAttacked;
        readonly EnemyParamData _enemyParamData;
        public EnemyAttack(EnemyParamData enemyParamData)
        {
            _enemyParamData = enemyParamData;
        }
        
        public void Attack(IHpSetter hpSetter)
        {
            hpSetter.CurrentHp -= _enemyParamData.GetAttackAmount();
            var playerMono = (PlayerMono)hpSetter;
            playerMono.OnAttacked(_enemyParamData.EnemyEnum());
        }
    } 
    public record PlayerOnAttackedEventArgs(EnemyEnum enemyEnum);
}

