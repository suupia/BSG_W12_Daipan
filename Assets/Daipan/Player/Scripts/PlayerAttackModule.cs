#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public static class PlayerAttackModule
    {
        public static void Attack(Hp hp, IPlayerParamData playerParamData)
        {
            hp.Decrease(playerParamData.GetAttack());
        }

        public static IEnumerable<EnemyEnum> GetTargetEnemyEnum(PlayerColor playerColor)
        {
            return playerColor switch
            {
                PlayerColor.Red => new[] {EnemyEnum.Red,EnemyEnum.RedBoss,EnemyEnum.Special,EnemyEnum.Totem2, EnemyEnum.Totem3},
                PlayerColor.Blue => new[] {EnemyEnum.Blue,EnemyEnum.BlueBoss,EnemyEnum.Special,EnemyEnum.Totem2, EnemyEnum.Totem3},
                PlayerColor.Yellow => new[] {EnemyEnum.Yellow,EnemyEnum.YellowBoss,EnemyEnum.Special,EnemyEnum.Totem2, EnemyEnum.Totem3},
                _ => throw new ArgumentOutOfRangeException()
            };
        } 

        
        public static bool IsInScreenEnemy(EnemyMono? enemyMono)
        {
            if (enemyMono == null) return false;
            var worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            return enemyMono.gameObject.transform.position.x < worldPosition.x;
        }
    }   
}

