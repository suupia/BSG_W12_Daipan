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
        public float Value { get; private set; }
        public IReadOnlyList<float> RatioTable => _irritatedParams.RatioTable;
        public int CurrentIrritatedStage
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
Debug.Log($"IrritatedValue.IncreaseValue() amount : {amount}");
            Value = Mathf.Min(MaxValue, Value + amount); 
        }

        public void DecreaseValue(float amount)
        {
            // [Precondition]
            if (amount < 0) Debug.LogWarning($"IrritatedValue.DecreaseValue() amount is negative : {amount}");

            Value = Mathf.Max(0, Value - amount);
        }
    }
}