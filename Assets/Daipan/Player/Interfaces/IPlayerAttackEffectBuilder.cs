#nullable enable
using System.Collections.Generic;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerAttackEffectBuilder
    {
        PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor);
    } 
}

