#nullable enable
using System;
using Fusion;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public struct Hp : INetworkStruct
    {
        public Hp(double value)
        {
            Value = value;
        }
        public double Value { get; private set; }
    }
}   