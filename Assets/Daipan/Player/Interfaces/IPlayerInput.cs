#nullable enable
using System.Collections.Generic;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerInput
    {
        public void SetPlayerMono(
            PlayerMono playerMono
            ,List<AbstractPlayerViewMono?> playerViewMonos
        );

        public void Update();
    } 
}

