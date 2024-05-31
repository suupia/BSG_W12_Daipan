#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Tower.Scripts
{
    public class TowerPositionMono : MonoBehaviour
    {
        [Header("タワー（プレイヤー）の座標たちをセットする場所です！")]
        [Space(30)]
        [Header("タワー（プレイヤー）の生成位置")]
        public Transform towerSpawnTransform = null!;
    }
}