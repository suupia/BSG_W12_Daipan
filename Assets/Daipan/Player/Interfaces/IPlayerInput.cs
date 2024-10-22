#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerInput
    {
        public void SetPlayerMono(
            IMonoBehaviour playerMono
            ,List<AbstractPlayerViewMono?> playerViewMonos
        );

        public void Update(float deltaTime);
    } 
}

