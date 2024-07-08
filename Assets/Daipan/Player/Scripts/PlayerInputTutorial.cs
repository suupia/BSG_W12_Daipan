#nullable enable
using System.Collections.Generic;
using Daipan.InputSerial.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerInputTutorial : IPlayerInput
    {
        readonly InputSerialManager _inputSerialManager; 
        readonly IAttackExecutor _attackExecutor;
        readonly DaipanExecutor _daipanExecutor;
        readonly SpeechEventManager _speechEventManager;
        PlayerMono? _playerMono;

        public PlayerInputTutorial(
            InputSerialManager inputSerialManager
            , IAttackExecutor attackExecutor
            , DaipanExecutor daipanExecutor
            , SpeechEventManager speechEventManager
        )
        {
            _inputSerialManager = inputSerialManager;
            _attackExecutor = attackExecutor;
            _daipanExecutor = daipanExecutor;
            _speechEventManager = speechEventManager;
        }

        public void SetPlayerMono(
            PlayerMono playerMono
            ,List<AbstractPlayerViewMono?> playerViewMonos
            )
        {
            _playerMono = playerMono;
            _attackExecutor.SetPlayerViewMonos(playerViewMonos);
        }

        /// <summary>
        /// Please call this method in Update method of MonoBehaviour
        /// </summary>
        public void Update()
        {
            Debug.Log($"_speechEventManager.GetSpeechEventEnum() = {_speechEventManager.GetSpeechEventEnum()} CurrentStep = {_speechEventManager.CurrentEvent.Message}"); 
            switch (_speechEventManager.GetSpeechEventEnum())
            {
                case SpeechEventEnum.Listening:
                    TutorialListeningUpdate();
                    break;
                case SpeechEventEnum.Practical:
                    TutorialPracticalUpdate();
                    break;
                case SpeechEventEnum.None:
                    break;
            }
        }

        void TutorialListeningUpdate()
        {
            if (_inputSerialManager.GetButtonAny())
            {
                _speechEventManager.MoveNext();
            } 
        }
        
        void TutorialPracticalUpdate()
        {
            if (_playerMono == null)
            {
                Debug.LogWarning("PlayerMono is not set");
                return;
            }
            
            if (_inputSerialManager.GetButtonRed())
            {
                Debug.Log("Wが押されたよ");
                _attackExecutor.FireAttackEffect(_playerMono, PlayerColor.Red);
            }

            if (_inputSerialManager.GetButtonBlue())
            {
                Debug.Log("Aが押されたよ");
                _attackExecutor.FireAttackEffect(_playerMono, PlayerColor.Blue);
            }

            if (_inputSerialManager.GetButtonYellow())
            {
                Debug.Log("Sが押されたよ");
                _attackExecutor.FireAttackEffect(_playerMono, PlayerColor.Yellow);
            }

            if (Input.GetKeyDown(KeyCode.Return))
            {
                _daipanExecutor.DaiPan();
                _speechEventManager.MoveNext();
            } 
        }
    }
}