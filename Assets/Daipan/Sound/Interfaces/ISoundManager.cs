#nullable enable
using Daipan.Sound.MonoScripts;

namespace Daipan.Sound.Interfaces
{
    public interface ISoundManager
    {
        void PlayBgm(BgmEnum bgmEnum);
        void PlaySe(SeEnum seEnum);
    } 
}

