#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine.EventSystems;

namespace Daipan.Enemy.Scripts
{
    public static class EnemyAttackModule
    {
        public static event EventHandler<DamageArgs>? AttackEvent;
        
        public static Hp Attack(IEnemyParamData enemyParamData, Hp hp)
        {
            AttackEvent?.Invoke( typeof(EnemyAttackModule) ,  new DamageArgs(enemyParamData.GetAttackAmount(), enemyParamData.GetEnemyEnum()));
            return new Hp(hp.Value - enemyParamData.GetAttackAmount()); 
        
        } 
    } 
}

