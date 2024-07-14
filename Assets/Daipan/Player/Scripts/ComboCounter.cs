#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class ComboCounter
    {
        public int ComboCount { get; private set; }
        public int MaxComboCount { get; private set; }

        public void IncreaseCombo()
        {
            ComboCount++;
            MaxComboCount = Math.Max(MaxComboCount, ComboCount);
        }

        public void ResetCombo()
        {
            ComboCount = 0;
        }


    }
}