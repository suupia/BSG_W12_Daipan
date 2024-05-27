#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Enemy.Scripts
{
    public class EnemyDefeatPositionMono : MonoBehaviour
    {
        [Header("Enemyを早く倒したかどうかを判定する位置")] public Transform enemyDefeatQuickPosition = null!;

        [Header("Enemyを遅く倒したかどうかを判定する位置")] public Transform enemyDefeatSlowPosition = null!;
    }
}
