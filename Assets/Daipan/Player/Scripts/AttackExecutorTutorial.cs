#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class AttackExecutorTutorial : IAttackExecutor
    {
        readonly IAttackExecutor _attackExecutor; 

        // 本当はDecoratorパターンを使いたいが、Resolveできないので、妥協
        public AttackExecutorTutorial(
            AttackExecutor attackExecutor
            ) 
        {
            _attackExecutor = attackExecutor;
        }

        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _attackExecutor.SetPlayerViewMonos(playerViewMonos); 
        }

        public void FireAttackEffect(IMonoBehaviour playerMono, PlayerColor playerColor)
        {
            _attackExecutor.FireAttackEffect(playerMono, playerColor); 
        }
    }
}