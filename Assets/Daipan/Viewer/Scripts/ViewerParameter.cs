#nullable enable
using System;
using UnityEngine;

namespace Daipan.Viewer.Scripts
{
    [Serializable]
    public sealed class ViewerNumberParameter
    {
        public int increaseNumberPerSecond;
    }
    
    [SerializeField]
    public sealed class DaipanParameter
    {
        public int increaseNumberByDaipan;
    }

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ViewerParameter", order = 1)]
    public sealed class ViewerParameter : ScriptableObject
    {
        [SerializeField] public ViewerNumberParameter ViewerNumberParameter;
        [SerializeField] public DaipanParameter DaipanParameter;
    }
}