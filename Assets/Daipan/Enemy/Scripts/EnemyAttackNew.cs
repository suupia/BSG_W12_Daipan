#nullable enable
using System;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine.EventSystems;

namespace Daipan.Enemy.Scripts
{
    public class EnemyAttackNew
    {
        public static event EventHandler<PlayerHpNew>? AttackEvent;
        
        public static PlayerHpNew Attack(IEnemyParamData enemyParamData, PlayerHpNew hp)
        {
            AttackEvent?.Invoke( typeof(EnemyAttackNew) , new PlayerHpNew(hp.Hp - enemyParamData.GetAttackAmount()));
            return new PlayerHpNew(hp.Hp - enemyParamData.GetAttackAmount()); 
        
        } 
    } 
}

