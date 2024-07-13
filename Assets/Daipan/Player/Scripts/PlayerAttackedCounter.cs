#nullable enable

using Daipan.Player.LevelDesign.Interfaces;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackedCounter
    {
        public bool IsOverThreshold => AttackedCountTotal - CurrentTermStartCount >= _threshold;
        int AttackedCountTotal { get; set; }
        int CurrentTermStartCount { get; set; }
        readonly int _threshold;

        public PlayerAttackedCounter(IPlayerAntiCommentParamData playerAntiCommentParamData)
        {
            _threshold = playerAntiCommentParamData.GetAntiCommentThreshold();
        }

        public void CountUp()
        {
            AttackedCountTotal++;

            if (AttackedCountTotal - CurrentTermStartCount > _threshold)
                CurrentTermStartCount = AttackedCountTotal;

        }

        public void ResetCount()
        {
            AttackedCountTotal = 0;
            CurrentTermStartCount = 0;
        } 
    }
}

