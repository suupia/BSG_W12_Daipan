#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using UnityEngine.Serialization;


namespace Daipan.Enemy.LevelDesign.Scripts
{
    public class EnemyPositionMono : MonoBehaviour
    {
        [Header("各Waveごとの敵の生成位置の候補")] 
        public List<EnemySpawnedPositionContainer> enemySpawnedPositionContainers = null!;
        

        [Header("敵の消滅位置")] [Tooltip("敵の消滅位置を示すGameObjectを入れて！！")]
        public Transform enemyDespawnedPoint = null!;
    }

    [Serializable]
    public class EnemySpawnedPositionContainer
    {
        public List<EnemySpawnedPosition> enemySpawnedPoints = null!;
    }

    [Serializable]
    public class EnemySpawnedPosition
    {
        [Header("敵の生成位置")] [Tooltip("敵の生成位置のx座標を決めるゲームオブジェクト")]
        public Transform enemySpawnTransformX = null!;

        [Tooltip("敵の生成位置のy座標を決めるゲームオブジェクト")] public Transform enemySpawnTransformY = null!;

        [Header("敵の生成される確率（相対的に設定できる)")] [Min(0)]
        public float enemySpawnRatio = 10;

        [FormerlySerializedAs("EnemyType")] [Header("生成される敵（指定しないとランダム）")]
        public EnemyEnum enemyEnum = EnemyEnum.None;
    }
}