#nullable enable
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerAttackEffectBuilder
    {
        PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, IMonoBehaviour playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor);
    } 
}

