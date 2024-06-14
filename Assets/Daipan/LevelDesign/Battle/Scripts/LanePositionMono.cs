#nullable enable
using System;
using System.Collections.Generic;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.LevelDesign.Battle.Scripts
{
    public class LanePositionMono : MonoBehaviour
    {
       [Header("レーンの座標に関する設定はここです")]
       [Space]
       public List<LanePosition> lanePositions = null!;
       
       [Header("敵の消滅位置")]
       public Transform enemyDespawnedPoint = null!;
       [Header("攻撃エフェクトの消滅位置")]
       public Transform attackEffectDespawnedPoint = null!;
    }

    [Serializable]
    public class LanePosition
    {
        [Header("レーンのy座標を決めるゲームオブジェクト")]
        public Transform laneYTransform = null!;

        [Header("プレイヤーの生成されるx座標を決めるゲームオブジェクト")]
        public Transform playerSpawnXTransform = null!;

        [Header("敵の生成されるx座標を決めるゲームオブジェクト")]
        public Transform enemySpawnXTransform = null!;
        
        [Header("敵の生成される確率（相対的に設定できる")]
        [Min(0)]
        public float enemySpawnRatio;

        [Header("生成される敵の種類（指定しないとランダム）")] 
        public EnemyEnum enemyEnum = EnemyEnum.None;

        // このクラスがWaveごとにあって、ListでそのWaveにおけるレーンを表してもいいかも。

    }
}