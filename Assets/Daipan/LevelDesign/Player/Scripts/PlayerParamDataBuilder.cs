#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;


namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamDataBuilder
    {
        public PlayerParamDataBuilder (
            IContainerBuilder builder,
            PlayerParam playerParam)
        {
            var playerParamData = new PlayerParamData
            {
                GetCurrentHp = () => playerParam.hpAmount,
                SetCurrentHp = (hp) => playerParam.hpAmount = hp,
                
                GetWAttack = () => playerParam.playerAttackParam.WAttackAmount,
                GetAAttack = () => playerParam.playerAttackParam.AAttackAmount,
                GetSAttack = () => playerParam.playerAttackParam.SAttackAmount
            };
            builder.RegisterInstance(playerParamData);
        }

    }
}