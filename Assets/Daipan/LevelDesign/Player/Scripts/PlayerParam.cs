#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Serialization;

namespace Daipan.LevelDesign.Player.Scripts
{



    [Serializable]
    public sealed class PlayerParam
    {
        public PlayerAttackParam playerAttackParam = null!;
        
        public PlayerColor playerColor = PlayerColor.None;
        public RuntimeAnimatorController attackEffectAnimatorController = null!;
    }
    
    [Serializable]
    public sealed class PlayerHpParam
    {
        [Header("プレイヤーのHP")]
        [Min(0)]
        public int hpAmount;
    }

    [Serializable]
    public sealed class PlayerAttackParam
    {
        [Min(0)] public int attackAmount = 10;
    }
    
    
}