#nullable enable
using System;
using Daipan.Enemy.Interfaces;

namespace Daipan.Battle.scripts
{
    public sealed class WaveState
    {
        public int CurrentWave { get; private set; } 

        public void NextWave()
        {
            CurrentWave++;
        }
    }
}