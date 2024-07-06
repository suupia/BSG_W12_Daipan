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

        readonly SpeechEventManager _speechEventManager;
        // 本当はDecoratorパターンを使いたいが、Resolveできないので、妥協
        public AttackExecutorTutorial(
            AttackExecutor attackExecutor
            ,SpeechEventManager speechEventManager
            ) 
        {
            _attackExecutor = attackExecutor;
            _speechEventManager = speechEventManager;
        }

        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _attackExecutor.SetPlayerViewMonos(playerViewMonos); 
        }

        public void FireAttackEffect(PlayerMono playerMono, PlayerColor playerColor)
        {
            // チュートリアルを聞いている時なら攻撃せずにテキストを送る
            Debug.Log($"AttackExecutorTutorial: _speechEventManager.GetSpeechEventEnum() = {_speechEventManager.GetSpeechEventEnum()}" +
                      $", SpeechEventEnum.Message = {_speechEventManager.CurrentEvent?.Message }");
            if (_speechEventManager.GetSpeechEventEnum() == SpeechEventEnum.Listening) return;
            _attackExecutor.FireAttackEffect(playerMono, playerColor); 
        }
    }
}