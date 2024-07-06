#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class AttackExecutorTutorial : IAttackExecutor
    {
        readonly IAttackExecutor _attackExecutor; 
        readonly RedEnemyTutorial _redEnemyTutorial; 
        // 本当はDecoratorパターンを使いたいが、Resolveできないので、妥協
        public AttackExecutorTutorial(
            AttackExecutor attackExecutor
            ,RedEnemyTutorial redEnemyTutorial
            ) 
        {
            _attackExecutor = attackExecutor;
            _redEnemyTutorial = redEnemyTutorial;
        }

        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _attackExecutor.SetPlayerViewMonos(playerViewMonos); 
        }

        public void FireAttackEffect(PlayerMono playerMono, PlayerColor playerColor)
        {
            // チュートリアルを聞いている時なら攻撃せずにテキストを送る
            if (_redEnemyTutorial.IsListeningTutorial) return;
            _attackExecutor.FireAttackEffect(playerMono, playerColor); 
        }
    }
}