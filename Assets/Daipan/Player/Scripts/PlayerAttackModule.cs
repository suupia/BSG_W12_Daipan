#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Battle.interfaces;
using Daipan.Enemy.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Scripts
{
    public static class PlayerAttackModule
    {
        public static event EventHandler<EnemyDamageArgs>? AttackEvent;
        public static Hp Attack(Hp hp, IPlayerParamData playerParamData)
        {
            AttackEvent?.Invoke( typeof(EnemyAttackModule) ,  new EnemyDamageArgs(playerParamData.GetAttack(), playerParamData.PlayerEnum()));
            return new Hp(hp.Value - playerParamData.GetAttack()); 
        
        } 
        
        public static IEnumerable<EnemyEnum> GetTargetEnemyEnum(PlayerColor playerColor)
        {
            return playerColor switch
            {
                PlayerColor.Red => new[] {EnemyEnum.Red,EnemyEnum.RedBoss,EnemyEnum.Special,EnemyEnum.Totem},
                PlayerColor.Blue => new[] {EnemyEnum.Blue,EnemyEnum.BlueBoss,EnemyEnum.Special,EnemyEnum.Totem},
                PlayerColor.Yellow => new[] {EnemyEnum.Yellow,EnemyEnum.YellowBoss,EnemyEnum.Special,EnemyEnum.Totem},
                _ => throw new ArgumentOutOfRangeException()
            };
        } 

    }  
}

