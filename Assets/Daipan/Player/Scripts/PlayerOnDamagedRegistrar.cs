#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.Scripts;
using Daipan.Comment.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Stream.Scripts;

namespace Daipan.Player.Scripts
{
    public class PlayerOnDamagedRegistrar
    {
        readonly IrritatedValue _irritatedValue;
        readonly PlayerAttackedCounter _playerAttackedCounter;
        readonly CommentSpawner _commentSpawner;
        
        public PlayerOnDamagedRegistrar
        (
            IrritatedValue irritatedValue
            , PlayerAttackedCounter playerAttackedCounter
            , CommentSpawner commentSpawner
        )
        {
            _irritatedValue = irritatedValue;
            _playerAttackedCounter = playerAttackedCounter;
            _commentSpawner = commentSpawner;
        }
        
        public void OnPlayerDamagedEvent
        (
            EnemyDamageArgs args
            , List<AbstractPlayerViewMono?> playerViewMonos
        )
        {
            // Domain
            _irritatedValue.IncreaseValue(args.DamageValue);

            // AntiComment
            _playerAttackedCounter.CountUp();
            if (_playerAttackedCounter.IsOverThreshold)
                _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);

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

