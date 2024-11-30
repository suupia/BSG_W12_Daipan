#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Stream.Interfaces
{
    public interface IIrritatedGaugeValue
    {
        public double MaxValue { get; }
        public bool IsFull => Value >= MaxValue;

        public double Ratio => Value / MaxValue;

        public double Value { get; }

        public IReadOnlyList<double> RatioTable { get; }

        public int CurrentIrritatedStage { get; }

        public void IncreaseValue(double amount);


        public void DecreaseValue(double amount);

        public void Reset();
        public void SetValue(double amount);
    }
}