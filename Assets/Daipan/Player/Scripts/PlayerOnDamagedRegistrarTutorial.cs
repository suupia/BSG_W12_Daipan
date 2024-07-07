#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.Scripts;
using Daipan.Comment.Scripts;
using Daipan.Player.Interfaces;

namespace Daipan.Player.Scripts
{
    public class PlayerOnDamagedRegistrarTutorial : IPlayerOnDamagedRegistrar
    {
        
        public PlayerOnDamagedRegistrarTutorial
        (
        )
        {
        }
        
        public void OnPlayerDamagedEvent
        (
            EnemyDamageArgs args
            , List<AbstractPlayerViewMono?> playerViewMonos
        )
        {
            // View
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(args.EnemyEnum))
                    playerViewMono.Damage();
            }
        }
    } 
}

