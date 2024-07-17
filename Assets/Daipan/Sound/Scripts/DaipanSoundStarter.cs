#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;

namespace Daipan.Sound.Scripts
{
    public class DaipanSoundStarter : IStart
    {
        readonly ISoundManager _soundManager;

        public DaipanSoundStarter(ISoundManager soundManager)
        {
            _soundManager = soundManager;
        }

        void IStart.Start()
        {
            _soundManager.PlayBgm(BgmEnum.Daipan);
        }
    }
}