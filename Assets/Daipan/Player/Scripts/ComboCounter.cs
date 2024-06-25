#nullable enable
using System.Collections.Generic;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Player.Scripts
{
    public sealed class ComboCounter
    {
        public int ComboCount { get; private set; }

        public void IncreaseCombo()
        {
            ComboCount++;
        }

        public void ResetCombo()
        {
            ComboCount = 0;
        }


    }
}