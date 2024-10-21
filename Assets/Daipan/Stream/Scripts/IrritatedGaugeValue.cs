#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core;
using Daipan.Daipan;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.Scripts
{
    public sealed class IrritatedGaugeValue
    {
        readonly IrritatedParams _irritatedParams;
        readonly DTONetWrapper _dtoNet;


        public IrritatedGaugeValue(double maxValue, IrritatedParams irritatedParams, DTONetWrapper dtoNet)
        {
            MaxValue = maxValue;
            _irritatedParams = irritatedParams;
            _dtoNet = dtoNet;
        }

        public double MaxValue { get; }
        public bool IsFull => Value >= MaxValue;
        public double Ratio => Value / MaxValue;

        public double Value
        {
            get => _dtoNet.IrritatedValue;
            private set => _dtoNet.IrritatedValue = value;
        }

        public IReadOnlyList<double> RatioTable => _irritatedParams.RatioTable;


        public int CurrentIrritatedStage
        {
            get
            {
                for (var i = 0; i < RatioTable.Count; i++)
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

        public void Reset()
        {
            Value = 0;
        }
    }
}