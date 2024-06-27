#nullable enable
using System;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Scripts 
{
    public class PlayerParamData : IPlayerParamData
    {
        readonly PlayerParam _playerParam;
        public PlayerParamData(PlayerParam playerParam)
        {
            _playerParam = playerParam; 
        }
        
        public UnityEngine.RuntimeAnimatorController? GetAnimator() => _playerParam.attackEffectAnimatorController;
        public PlayerColor PlayerEnum() => _playerParam.playerColor;
        public int GetAttack() => _playerParam.playerAttackParam.attackAmount;

    }

    public class PlayerHpParamData : IPlayerHpParamData
    {
       readonly PlayerParamManager _playerParamManager;
       public PlayerHpParamData(PlayerParamManager playerParamManager)
       {
           _playerParamManager = playerParamManager;
       }
       public int GetCurrentHp() => _playerParamManager.playerHpParam.hpAmount;
       public int SetCurrentHp(int value) => _playerParamManager.playerHpParam.hpAmount = value;
       public int GetAntiCommentThreshold() => _playerParamManager.playerHpParam.antiCommentThreshold;
       
    }
}