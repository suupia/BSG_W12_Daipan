#nullable enable

namespace Daipan.Player.Scripts
{
    public class PlayerAttackedCounter
    {
        public int AttackedNumber;
        public bool IsOverThreshold;

        int _currentTermStartNumber;
        readonly int _threshold;

        public PlayerAttackedCounter(int threshold)
        {
            _threshold = threshold;
            CountReset();
        }

        public void CountUp()
        {
            AttackedNumber++;

            if (AttackedNumber - _currentTermStartNumber >= _threshold)
            {
                IsOverThreshold = true;
                _currentTermStartNumber = AttackedNumber;
            }
            else
            {
                IsOverThreshold = false;
            }
        }

        public void CountReset()
        {
            AttackedNumber = 0;
            _currentTermStartNumber = 0;
            IsOverThreshold = false;
        } 
    }
}

