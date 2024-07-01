#nullable enable
using System;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine.EventSystems;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttackNew
    {
        public static event EventHandler<DamageArgs>? AttackEvent;
        
        public static PlayerHpNew Attack(IEnemyParamData enemyParamData, PlayerHpNew hp)
        {
            AttackEvent?.Invoke( typeof(EnemyAttackNew) ,  new DamageArgs(enemyParamData.GetAttackAmount(), enemyParamData.GetEnemyEnum()));
            return new PlayerHpNew(hp.Hp - enemyParamData.GetAttackAmount()); 
        
        } 
    } 
}

