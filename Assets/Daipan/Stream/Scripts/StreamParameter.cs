#nullable enable
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Stream.Viewer.Scripts
{
    [Serializable]
    public sealed class ViewerParameter
    {
        public int increaseNumberPerSecond;
        public int decreaseNumberWhenIrradiated;
    }

    [Serializable]
    public sealed class DaipanParameter
    {
        public int increaseNumberByDaipan;
        public int increaseNumberWhenExciting;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ViewerParameter", order = 1)]
    public sealed class StreamParameter : ScriptableObject
    {
        [FormerlySerializedAs("viewerNumberParameter")] [SerializeField] public ViewerParameter viewerParameter = null!;
        [SerializeField] public DaipanParameter daipanParameter = null!;
    }
}