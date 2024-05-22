#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyPosition : MonoBehaviour
    {
        [Header("エネミーの座標たちをセットする場所です！")]
        [Space(30)]


        
        public List<EnemySpawnedPositon> enemySpawnedPoints;


        [Header("エネミーの消滅位置")]
        [Tooltip("エネミーの消滅位置を示すGameObjectを入れて！！")]
        public Transform enemyDespawnedPoint;

    }

    [Serializable]
    public class EnemySpawnedPositon
    {
        [Header("エネミーの生成位置")]
        [Tooltip("エネミーの生成位置を示すGameObjectを入れて！！")]
        public Transform transform;
        [Header("エネミーの生成される確率（相対的に設定してよい)")]
        [Min(0)]
        public float ratio;
    }

}
