#nullable enable
using System;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts 
{
    public sealed class PlayerParamData : IPlayerParamData
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

    public sealed class PlayerHpParamData : IPlayerHpParamData
    {
       readonly PlayerParamManager _playerParamManager;
       public PlayerHpParamData(PlayerParamManager playerParamManager)
       {
           _playerParamManager = playerParamManager;
       }
       public int GetMaxHp() => _playerParamManager.playerHpParam.maxHpAmount;
       public int GetCurrentHp() => _playerParamManager.playerHpParam.hpAmount;
       public int SetCurrentHp(int value) => _playerParamManager.playerHpParam.hpAmount = value;
       
    }
    public sealed class PlayerAntiCommentParamData  : IPlayerAntiCommentParamData
    {
        readonly PlayerParamManager _playerParamManager;
        public PlayerAntiCommentParamData(PlayerParamManager playerParamManager)
        {
            _playerParamManager = playerParamManager;
        }
        public int GetAntiCommentThreshold() => _playerParamManager.playerAntiCommentParam.antiCommentThreshold;
        public double GetAntiCommentPercentOnMissAttacks(int index)
        {
            if (index < 0 || index >= _playerParamManager.playerAntiCommentParam.antiCommentPercentOnMissAttacks.Count)
            {
                Debug.LogWarning($"index out of range. index : {index}");
            }
            return _playerParamManager.playerAntiCommentParam.antiCommentPercentOnMissAttacks[index];
        }
    }
}