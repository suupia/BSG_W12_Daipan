#nullable enable
using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerMissedAttackCounter
    {
        public bool IsOverThreshold => AttackedNumber - CurrentTermStartCount >= _threshold;
        int AttackedNumber { get; set; }
        int CurrentTermStartCount { get; set; }
        readonly int _threshold;

        public PlayerMissedAttackCounter(IPlayerAntiCommentParamData playerAntiCommentParamData)
        {
            _threshold = playerAntiCommentParamData.GetMissedAttackCountForAntiComment();
        }

        public void CountUp()
        {
            AttackedNumber++;

            if (AttackedNumber - CurrentTermStartCount > _threshold)
                CurrentTermStartCount = AttackedNumber;
        }

        void ResetCount()
        {
            AttackedNumber = 0;
            CurrentTermStartCount = 0;
        } 
    } 
}

