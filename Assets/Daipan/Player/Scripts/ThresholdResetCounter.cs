#nullable enable

using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Scripts
{
    public sealed class ThresholdResetCounter
    {
        public bool IsOverThreshold => TotalCount - LastResetCount >= _threshold;
        int TotalCount { get; set; }
        int LastResetCount { get; set; }
        readonly int _threshold;
        
        public ThresholdResetCounter(int threshold)
        {
            _threshold = threshold;
        }

        public void CountUp()
        {
            TotalCount++;

            if (TotalCount - LastResetCount > _threshold)
                LastResetCount = TotalCount;

        }

        public void ResetCount()
        {
            TotalCount = 0;
            LastResetCount = 0;
        } 
    }
}

