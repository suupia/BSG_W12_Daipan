#nullable enable
using System;
using System.Collections.Generic;
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
        [Header("プレイヤーや敵の生成される高さ")]
        public Transform transform = null!;
    }
}