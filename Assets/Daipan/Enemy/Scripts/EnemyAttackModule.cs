#nullable enable
using System;
using Daipan.Battle.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine.EventSystems;

namespace Daipan.Enemy.Scripts
{
    public static class EnemyAttackModule
    {
        public static event EventHandler<EnemyDamageArgs>? AttackEvent;
        
        public static Hp Attack(IEnemyParamData enemyParamData, Hp hp)
        {
            AttackEvent?.Invoke( typeof(EnemyAttackModule) ,  new EnemyDamageArgs(enemyParamData.GetAttackAmount(), enemyParamData.GetEnemyEnum()));
            return new Hp(hp.Value - enemyParamData.GetAttackAmount()); 
        
        }

        public static void AttackNew(PlayerMono playerMono, IEnemyParamData enemyParamData)
        {
            playerMono.OnAttacked(enemyParamData);
        }
        
    } 
}

