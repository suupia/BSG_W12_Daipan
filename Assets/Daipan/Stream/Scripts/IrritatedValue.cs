#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class IrritatedValue
    {
        readonly IrritatedParams _irritatedParams;

        public IrritatedValue(double maxValue, IrritatedParams irritatedParams)
        {
            MaxValue = maxValue;
            _irritatedParams = irritatedParams;
        }

        public double MaxValue { get; }
        public bool IsFull => Value >= MaxValue;
        public double Ratio => Value / MaxValue;
        public double Value { get; private set; }
        public IReadOnlyList<double> RatioTable => _irritatedParams.RatioTable;

        public int CurrentIrritatedStage
        {
            get
            {
                for (int i = 0; i < RatioTable.Count; i++)
                {
                    if (Ratio >= RatioTable[i]) continue;
                    return i;
                }

                return RatioTable.Count;
            }
        }


        public void IncreaseValue(double amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.IncreaseValue() amount is negative : {amount}");
            // Debug.Log($"IrritatedValue.IncreaseValue() amount : {amount}");
            Value = Math.Min(MaxValue, Value + amount);
        }

        public void DecreaseValue(double amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.DecreaseValue() amount is negative : {amount}");

            Value = Math.Max(0, Value - amount);
        }
    }
}