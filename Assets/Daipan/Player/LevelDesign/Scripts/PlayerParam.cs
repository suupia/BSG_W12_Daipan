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
        [Header("プレイヤーの最大HP")]
        [Min(0)]
        public int maxHpAmount = 100;
        [Header("プレイヤーの現在のHP")]
        [Min(0)]
        public int hpAmount;
    }

    [Serializable]
    public sealed class PlayerAntiCommentParam
    {
        [Header("n回攻撃されたらアンチコメント")]
        [Min(0)]
        public int antiCommentThreshold;

        [Header("n回攻撃をミスしたらアンチコメントが生成される")] [Min(0)]
        public int missedAttackCountForAntiComments = 10;

        [Header("Waveごとの、異なる敵に攻撃をしてしまった時にアンチコメントが生成される確率")]
        public List<double> antiCommentPercentOnMissAttacks = null!;

        [Header("ラスボスに異なる攻撃をしてしまったときにアンチコメントが生成される確率")] [Min(0)]
        public int finalBossAntiCommentPercentOnMissAttacks = 30;

    }

    [Serializable]
    public sealed class PlayerAttackParam
    {
        [Min(0)] public int attackAmount = 10;
    }
    
    
}