using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Daipan.Stream.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stream/IrritatedParameters", order = 1)]
    public sealed class IrritatedParams : ScriptableObject
    {
        [Header("イライラゲージのレベルデザインはこちら")]
        [Space(30)]


        [Min(0)] public float[] RatioTable;
    }
}
