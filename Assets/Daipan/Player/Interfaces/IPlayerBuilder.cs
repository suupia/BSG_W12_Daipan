#nullable enable
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerBuilder
    {
        IPlayerMono Build(IPlayerMono playerMono);
    } 
}

