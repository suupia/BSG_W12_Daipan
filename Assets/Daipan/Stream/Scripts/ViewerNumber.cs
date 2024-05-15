﻿#nullable enable
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public class ViewerNumber
    {
        public float Ratio => (float)Number / MaxNumber;
        public int Number => Mathf.Max(IncreasedNumber - DecreasedNumber, 0);
        int DecreasedNumber { get; set; }
        int IncreasedNumber { get; set; }
        int MaxNumber => 10_000;

        public void IncreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.IncreaseViewer() amount is negative : {amount}");
            IncreasedNumber += amount;
        }

        public void DecreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.DecreaseViewer() amount is negative : {amount}");

            if (Number <= 0) return;
            if (Number - amount < 0)
                DecreasedNumber += Number;
            else
                DecreasedNumber += amount;
        }
    }
}