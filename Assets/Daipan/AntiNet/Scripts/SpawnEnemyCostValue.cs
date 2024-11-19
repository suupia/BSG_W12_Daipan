#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.AntiNet.Scripts
{
    public class SpawnEnemyCostValue
    {
        // todo 値をParamsにまとめる
        readonly int MaxValue = 20;
        readonly int initializedValue = 15;
        //
        int value;

        public int Value { get => value; }
        SpawnEnemyCostValue()
        {
            value = initializedValue;
        }

        public void IncreaseValue(int amount)
        {
            if (amount < 0) return;
            value = Math.Min(MaxValue, value + amount);
        }

        public void DecreaseValue(int amount)
        {
            if (amount < 0) return;
            value = Math.Max(0, value - amount);
        }
    }
}