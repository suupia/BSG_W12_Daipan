#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using UnityEngine;
using UnityEngine.Networking.PlayerConnection;
using UnityEngine.Serialization;

namespace Daipan.Player.LevelDesign.Scripts
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
        [Header("n回攻撃されたらアンチコメント")]
        [Min(0)]
        public int antiCommentThreshold;
    }

    [Serializable]
    public sealed class PlayerAttackParam
    {
        [Min(0)] public int attackAmount = 10;
    }
    
    
}