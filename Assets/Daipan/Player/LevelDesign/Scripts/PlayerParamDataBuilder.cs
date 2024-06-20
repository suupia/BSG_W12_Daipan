#nullable enable
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.Scripts;
//using Unity.VisualScripting.YamlDotNet.Core.Tokens;
using UnityEngine;
using VContainer;


namespace Daipan.Player.LevelDesign.Scripts
{
    public class PlayerParamDataBuilder
    {
        public PlayerParamDataBuilder (
            IContainerBuilder builder,
            PlayerParamManager playerParamManager)
        {
            var playerParams = new List<PlayerParamData>();
            foreach (var playerParam in playerParamManager.playerParams)
            {
                playerParams.Add(new PlayerParamData()
                {
                    GetAnimator = () => playerParam.attackEffectAnimatorController,
                    PlayerEnum = () => playerParam.playerColor,
                    GetAttack = () => playerParam.playerAttackParam.attackAmount
                });  
            }
            var playerParamContainer = new PlayerParamDataContainer(playerParams);
            builder.RegisterInstance(playerParamContainer);
            
            builder.RegisterInstance(new PlayerHpParamData()
            {
                GetCurrentHp  = () => playerParamManager.playerHpParam.hpAmount,
                SetCurrentHp = value => playerParamManager.playerHpParam.hpAmount = value
            });
        }

    }
}