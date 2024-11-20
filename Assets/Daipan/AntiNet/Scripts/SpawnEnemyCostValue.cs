#nullable enable
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class SpawnEnemyCostValue
    {
        readonly int _maxValue;

        readonly int _initializedValue;
        //
        int value;

        public int Value { get => value; }

        [Inject]
        public SpawnEnemyCostValue(ICostValueParam costValueParam)
        {
            _maxValue = costValueParam.MaxCostValue;
            _initializedValue = costValueParam.InitializedCostValue;
            value = _initializedValue;
        }

        public void IncreaseValue(int amount)
        {
            if (amount < 0) return;
            value = Math.Min(_maxValue, value + amount);
        }

        public void DecreaseValue(int amount)
        {
            if (amount < 0) return;
            value = Math.Max(0, value - amount);
        }
    }
}