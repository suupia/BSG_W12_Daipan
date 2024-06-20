#nullable enable
using System;

namespace Daipan.Player.LevelDesign.Scripts
{
    [Serializable]
    public class ComboParam
    {
        public int comboThreshold = 10; 
        public double comboMultiplier = 1.05;
    }
}