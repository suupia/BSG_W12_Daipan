#nullable enable
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerBuilder
    {
        PlayerMono Build(PlayerMono playerMono);
    } 
}

