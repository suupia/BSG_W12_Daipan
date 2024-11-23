#nullable enable

namespace Daipan.AntiNet.Scripts
{
    public class AntiStateValue
    {
        public AntiStateEnum AntiStateEnum { get => _antiStateEnum; }

        private AntiStateEnum _antiStateEnum;

        public AntiStateValue()
        {
            SetNormal();
        }


        public void SetNormal()
        {
            _antiStateEnum = AntiStateEnum.Normal;
        }
        public void SetBan()
        {
            _antiStateEnum = AntiStateEnum.BAN;
        }
        public void SetFever()
        {
            _antiStateEnum = AntiStateEnum.FEVER;
        }

    }
}