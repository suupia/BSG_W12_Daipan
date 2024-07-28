#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Sound.MonoScripts;

namespace Daipan.Sound.Scripts
{
    public class DaipanSoundStarter : IStart
    {
        void IStart.Start()
        {
            SoundManager.Instance?.PlayBgm(BgmEnum.Daipan);
        }
    }
}