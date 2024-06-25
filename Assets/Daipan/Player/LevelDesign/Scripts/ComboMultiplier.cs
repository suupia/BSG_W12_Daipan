#nullable enable
using System;
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.LevelDesign.Scripts
{
    public class ComboMultiplier : IComboMultiplier
    {
        public double CalculateComboMultiplier(int comboCount)
        {
            return 1 + comboCount / 100.0;
        }
    }
}