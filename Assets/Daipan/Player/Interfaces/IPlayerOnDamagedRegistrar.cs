#nullable enable
using System.Collections.Generic;
using Daipan.Battle.Scripts;

namespace Daipan.Player.Interfaces
{
    public interface IPlayerOnDamagedRegistrar
    {
        void OnPlayerDamagedEvent(
            EnemyDamageArgs args
            , List<AbstractPlayerViewMono?> playerViewMonos
        );
    } 
}

