#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using UnityEngine;

namespace Daipan.Player.Scripts
{
   
    public class PlayerOnAttackedTutorial : IPlayerOnAttacked
    {
        List<AbstractPlayerViewMono?>? _playerViewMonos; 
        
        public void SetPlayerViews(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _playerViewMonos = playerViewMonos;
        }
        public Hp OnAttacked(Hp hp, IEnemyParamData enemyParamData)
        {
                        
            // view
            if (_playerViewMonos == null)
            {
                Debug.LogWarning("PlayerViewMonos is null");
                return new Hp(hp.Value - enemyParamData.GetAttackAmount());
            }
            foreach (var playerViewMono in _playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(enemyParamData.GetEnemyEnum()))
                    playerViewMono.Damage();
            }            
            return new Hp(hp.Value - enemyParamData.GetAttackAmount());
        }
    } 
}
