#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class AttackExecutorTutorial : IAttackExecutor
    {
        readonly IAttackExecutor _attackExecutor; 
        public AttackExecutorTutorial(
            IAttackExecutor attackExecutor 
            )
        {
            _attackExecutor = attackExecutor;
        }

        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _attackExecutor.SetPlayerViewMonos(playerViewMonos); 
        }

        public void FireAttackEffect(PlayerMono playerMono, PlayerColor playerColor)
        {
            // todo:チュートリアル中なら攻撃しない
            _attackExecutor.FireAttackEffect(playerMono, playerColor); 
        }
    }
}