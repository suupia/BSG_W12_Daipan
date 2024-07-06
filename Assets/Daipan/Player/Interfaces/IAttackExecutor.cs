#nullable enable
using System.Collections.Generic;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IAttackExecutor
    {
        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos);
        public void FireAttackEffect(PlayerMono playerMono, PlayerColor playerColor);
    }
}