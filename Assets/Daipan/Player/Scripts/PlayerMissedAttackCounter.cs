#nullable enable
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerMissedAttackCounter
    {
        int AttackedNumber { get; set; }
        public bool IsOverThreshold;

        int _currentTermStartNumber;
        readonly int _threshold;

        public PlayerMissedAttackCounter(IPlayerAntiCommentParamData playerAntiCommentParamData)
        {
            _threshold = playerAntiCommentParamData.GetMissedAttackCountForAntiComment();
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

