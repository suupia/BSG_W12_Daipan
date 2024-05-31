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

        [Inject]
        PlayerParamConfig (PlayerParam playerParam)
        {
            _playerParam = playerParam;
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