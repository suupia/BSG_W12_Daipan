#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;


namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamBuilder
    {
        readonly PlayerParam _playerParam;

        public PlayerParamBuilder (
            IContainerBuilder builder,
            PlayerParam playerParam)
        {
            _playerParam = playerParam;
            
            var playerParamDTO = new PlayerParamData
            {
                GetCurrentHp = () => _playerParam.hpAmount,
                SetCurrentHp = (hp) => _playerParam.hpAmount = hp,
                
                GetWAttack = () => playerParam.playerAttackParam.WAttackAmount,
                GetAAttack = () => playerParam.playerAttackParam.AAttackAmount,
                GetSAttack = () => playerParam.playerAttackParam.SAttackAmount
            };
            builder.RegisterInstance(playerParamDTO);
        }


        public int GetHpAmount()
        {
            return _playerParam.hpAmount;
        }

    }
}