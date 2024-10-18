#nullable enable
using System;
using Daipan.Battle.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine.EventSystems;

namespace Daipan.Enemy.Scripts
{
    public static class EnemyAttackModule
    {
        public static void Attack(IPlayerMono playerMono, IEnemyParamData enemyParamData)
        {
            playerMono.OnAttacked(enemyParamData);
        }
        
    } 
}

