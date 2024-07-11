#nullable enable
using System;
using UnityEngine;

namespace Daipan.Enemy.LevelDesign.Scripts
{
    [Serializable]
    public sealed class FinalWaveParam
    {
        [Header("FinalWaveの開始時刻")] [Min(0)]
        public float startTime = 0f;

    } 
}

