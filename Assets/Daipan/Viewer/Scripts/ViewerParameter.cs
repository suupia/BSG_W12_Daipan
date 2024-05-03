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

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PlayerAttackParameter", order = 1)]
    public sealed class ViewerParameter : ScriptableObject
    {
        [SerializeField] public ViewerNumberParameter ViewerNumberParameter;
    }
}