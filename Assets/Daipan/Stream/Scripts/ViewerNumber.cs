#nullable enable
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class ViewerNumber
    {
        public float Ratio => (float)Number / MaxNumber;
        public int Number => Mathf.Max(IncreasedNumber - DecreasedNumber, 0);
        int DecreasedNumber { get; set; }
        int IncreasedNumber { get; set; }
        int MaxNumber => 10_000;

        readonly ComboCounter _comboCounter;
        readonly IComboMultiplier _comboMultiplier;

        public ViewerNumber(
            ComboCounter comboCounter,
            IComboMultiplier comboMultiplier
            )
        {
            _comboCounter = comboCounter;
            _comboMultiplier = comboMultiplier;
        }

        public void IncreaseViewer(int amount)
        {
            // [Prerequisite]
            if (amount < 0) Debug.LogWarning($"ViewerNumber.IncreaseViewer() amount is negative : {amount}");
            
            var multipliedAmount = (int)(amount * _comboMultiplier.CalculateComboMultiplier(_comboCounter.ComboCount));
            
            IncreasedNumber += multipliedAmount;
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