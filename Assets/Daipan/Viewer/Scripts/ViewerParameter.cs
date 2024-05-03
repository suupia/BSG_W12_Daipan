#nullable enable
using System;
using UnityEngine;

namespace Daipan.Viewer.Scripts
{
    [Serializable]
    public sealed class ViewerNumberParameter
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
    public sealed class ViewerParameter : ScriptableObject
    {
        [SerializeField] public ViewerNumberParameter viewerNumberParameter = null!;
        [SerializeField] public DaipanParameter daipanParameter = null!;
    }
}