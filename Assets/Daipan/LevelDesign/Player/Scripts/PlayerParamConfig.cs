#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VContainer;


namespace Daipan.LevelDesign.Player.Scripts
{
    public class PlayerParamConfig
    {
        readonly PlayerParam _playerParam;


        public PlayerParamConfig (
            IContainerBuilder builder,
            PlayerParam playerParam)
        {
            _playerParam = playerParam;
            
            var playerParamDTO = new PlayerParamDTO
            {
                GetWAttack = () => playerParam.playerAttackParam.WAttackAmount,
                GetAAttack = () => playerParam.playerAttackParam.AAttackAmount,
                GetSAttack = () => playerParam.playerAttackParam.SAttackAmount
            };
            Debug.Log($"builder = {builder}");
            builder.RegisterInstance(playerParamDTO);
        }


        public int GetHpAmount()
        {
            return _playerParam.hpAmount;
        }
        public PlayerAttackParam GetAttackAmount()
        {
            return _playerParam.playerAttackParam;
        }
    }
}