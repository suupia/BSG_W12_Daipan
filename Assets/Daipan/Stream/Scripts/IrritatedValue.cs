#nullable enable
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class IrritatedValue
    {
        readonly IrritatedParams _irritatedParams;

        public IrritatedValue(int maxValue, IrritatedParams irritatedParams)
        {
            MaxValue = maxValue;
            _irritatedParams = irritatedParams;
        }

        public int MaxValue { get; }
        public bool IsFull => Value >= MaxValue;
        public float Ratio => (float)Value / MaxValue;
        public float Value => Mathf.Max(IncreasedValue - DecreasedValue, 0);
        float DecreasedValue { get; set; }
        float IncreasedValue { get; set; }
        public IReadOnlyList<float> RatioTable => _irritatedParams.RatioTable;
        public float CurrentIrritatedStage
        {
            get
            {
                for(int i = 0; i < RatioTable.Count; i++)
                {
                    if (Ratio >= RatioTable[i]) continue;
                    return i;
                }
                return RatioTable.Count;
            }
        }
            

        public void IncreaseValue(float amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.IncreaseValue() amount is negative : {amount}");

            IncreasedValue += amount;
            if (Value >= MaxValue) IncreasedValue = MaxValue;

            Debug.Log($"IncreaseValue(amount: {amount}) IrritatedValue : {Value}");
        }

        public void DecreaseValue(float amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.DecreaseValue() amount is negative : {amount}");

            if (Value <= 0) return;
            if (Value - amount < 0)
                DecreasedValue += Value;
            else
                DecreasedValue += amount;
        }
    }
}