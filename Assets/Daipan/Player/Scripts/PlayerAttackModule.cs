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
        public static void Attack(EnemyMono enemyMono, IPlayerParamData playerParamData)
        {
            enemyMono.OnAttacked(playerParamData);
        }

        public static IEnumerable<EnemyEnum> GetTargetEnemyEnum(PlayerColor playerColor)
        {
            // 全てのボタンに対して判定を行うもの
            var result = new List<EnemyEnum>{EnemyEnum.SpecialRed,EnemyEnum.SpecialBlue,EnemyEnum.SpecialYellow, EnemyEnum.Totem2, EnemyEnum.Totem3};
            result.AddRange(  playerColor switch
            {
                PlayerColor.Red => new[] {EnemyEnum.Red,EnemyEnum.RedBoss},
                PlayerColor.Blue => new[] {EnemyEnum.Blue,EnemyEnum.BlueBoss},
                PlayerColor.Yellow => new[] {EnemyEnum.Yellow,EnemyEnum.YellowBoss},
                _ => throw new ArgumentOutOfRangeException()
            });
            return result;
        } 
        
        public static bool IsInStreamScreen(Vector3 position)
        {
            var worldPosition = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
            return position.x < worldPosition.x;
        }
    }   
}

