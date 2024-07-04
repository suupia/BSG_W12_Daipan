#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.LevelDesign.Battle.Scripts
{
    public sealed class LanePositionMono : MonoBehaviour
    {
        [Header("レーンの座標に関する設定はここです")] [Space] [Header("各Waveごとに使用するレーンの設定")]
        public List<LanePositionContainer> lanePositionContainers = null!;

        [Header("プレイヤーの生成位置のx座標を決めるゲームオブジェクト")]
        public PlayerSpawnedPosition playerSpawnedPosition = null!;

        [Header("敵の消滅位置")] public Transform enemyDespawnedPoint = null!;

        [Header("攻撃エフェクトの生成位置")] public Transform attackEffectSpawnedPoint = null!;
        [Header("攻撃エフェクトの消滅位置")] public Transform attackEffectDespawnedPoint = null!;
    }

    [Serializable]
    public sealed class LanePositionContainer
    {
        public List<LanePosition> lanePositions = new();
    }

    [Serializable]
    public sealed class LanePosition
    {
        [Header("レーンのy座標を決めるゲームオブジェクト")] public Transform laneYTransform = null!;

        public EnemySpawnedPosition enemySpawnedPosition = null!;
        // このクラスがWaveごとにあって、ListでそのWaveにおけるレーンを表してもいいかも。
    }
}