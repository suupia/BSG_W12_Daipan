#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.Scripts;
using Daipan.Utility.Scripts;
using UnityEngine;
using UnityEngine.Serialization;


namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyPositionMono : MonoBehaviour
    {
        [Header("敵の座標に関する設定はここです")]
        [Space]
        
        public List<EnemySpawnedPosition> enemySpawnedPoints = null!;
        
        [Header("敵の消滅位置")]
        [Tooltip("敵の消滅位置を示すGameObjectを入れて！！")]
        public Transform enemyDespawnedPoint = null!;

    }

    [Serializable]
    public class EnemySpawnedPosition
    {
        [Header("敵の生成位置")]
        [Tooltip("敵の生成位置を示すGameObjectを入れて！！")]
        public Transform enemySpawnTransformX = null!;
        [Header("敵の生成される確率（相対的に設定してよい)")]
        [Min(0)]    
        public float ratio;
        [FormerlySerializedAs("EnemyType")] [Header("生成される敵（指定しないとランダム）")]
        public EnemyEnum enemyEnum  = EnemyEnum.None;
        
     }

}
