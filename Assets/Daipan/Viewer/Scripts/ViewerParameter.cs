﻿#nullable enable
using System;
using UnityEngine;

namespace Daipan.Viewer.Scripts
{
    [Serializable]
    public sealed class ViewerNumberParameter
    {
        public int increaseNumberPerSecond;
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
        [SerializeField] public ViewerNumberParameter ViewerNumberParameter;
        [SerializeField] public DaipanParameter DaipanParameter;
    }
}