#nullable enable

using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackedCounter
    {
        public int AttackedNumber;
        public bool IsOverThreshold;

        int _currentTermStartNumber;
        readonly int _threshold;

        public PlayerAttackedCounter(IPlayerHpParamData playerHpParamData)
        {
            _threshold = playerHpParamData.GetAntiCommentThreshold();
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

