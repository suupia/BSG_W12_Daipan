#nullable enable

namespace Daipan.Player.Scripts
{
    public class PlayerAttackedCounter
    {
        public int AttackedNumber;
        public bool isOverThreshold;

        int _currentTermStartNumber;
        int _threshold;

        public PlayerAttackedCounter(int threshold)
        {
            _threshold = threshold;
            CoutnReset();
        }

        public void CountUp()
        {
            AttackedNumber++;

            if (AttackedNumber - _currentTermStartNumber >= _threshold)
            {
                isOverThreshold = true;
                _currentTermStartNumber = AttackedNumber;
            }
            else
            {
                isOverThreshold = false;
            }
        }

        public void CoutnReset()
        {
            AttackedNumber = 0;
            _currentTermStartNumber = 0;
            isOverThreshold = false;
        } 
    }
}

