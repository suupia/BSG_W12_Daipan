#nullable enable
using System;
using System.Collections.Generic;
using Daipan.LevelDesign.Battle.Scripts;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerPositionMono : MonoBehaviour
    {
        [Header("プレイヤーの座標に関する設定はここです")]
        [Space]
        public PlayerSpawnedPosition playerSpawnedPoint = null!;
        public PlayerAttackEffectPosition  playerAttackEffectPosition = null!;
    }

    [Serializable]
    public sealed class PlayerAttackEffectPosition
    {
        [Header("攻撃エフェクトの生成位置")]
        public Transform attackEffectSpawnedPoint = null!;
        [Header("攻撃エフェクトの消滅位置")]
        public Transform attackEffectDespawnedPoint = null!; 
    }
    
    [Serializable]
    public sealed class PlayerSpawnedPosition
    {
        [Header("プレイヤーの生成位置")]
        
        [Tooltip("プレイヤーの生成位置のx座標を決めるゲームオブジェクト")]
        public Transform playerSpawnTransformX = null!;

    }
}