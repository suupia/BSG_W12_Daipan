#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Daipan.Stream.Scripts
{
    [Serializable]
    public sealed class ViewerParam
    {
        [Header("一秒間に増える視聴者の数")] public int increaseNumberPerSecond;

        [Header("理不尽な状態の時の一秒間に減る視聴者の数")] public int decreaseNumberWhenIrradiated;
    }

    [Serializable]
    public sealed class DaipanParam
    {
        [Header("台パンによって増える視聴者の数")] public int increaseNumberByDaipan;

        [Header("盛り上がっているときに増える視聴者の数")] public int increaseNumberWhenExciting;
    }

    [Serializable]
    public sealed class TimerParam
    {
        [Header("最大時間")] public double maxTime;
    }
    
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Stream/SteamParam", order = 1)]
    public sealed class StreamParam : ScriptableObject
    {
        [SerializeField]
        public ViewerParam viewer = null!;

        [SerializeField]
        public DaipanParam daipan = null!;
        
        [SerializeField]
        public TimerParam timer = null!;
    }
}