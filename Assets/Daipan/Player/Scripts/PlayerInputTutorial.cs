#nullable enable
using System.Collections.Generic;
using Daipan.InputSerial.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
using Daipan.Tutorial.Scripts;
using UnityEngine;
using Daipan.Core.Interfaces;

namespace Daipan.Player.Scripts
{
    public class PlayerInputTutorial : IPlayerInput
    {
        readonly InputSerialManager _inputSerialManager;
        readonly IAttackExecutor _attackExecutor;
        readonly DaipanExecutor _daipanExecutor;
        readonly SpeechEventManager _speechEventManager;
        readonly IInputOption _inputOption;
        readonly IGetEnterKey _getEnterKey;

        IMonoBehaviour? _playerMono;

        public PlayerInputTutorial(
            InputSerialManager inputSerialManager
            , IAttackExecutor attackExecutor
            , DaipanExecutor daipanExecutor
            , SpeechEventManager speechEventManager
            , IInputOption inputOption
            , IGetEnterKey getEnterKey
        )
        {
            _inputSerialManager = inputSerialManager;
            _attackExecutor = attackExecutor;
            _daipanExecutor = daipanExecutor;
            _speechEventManager = speechEventManager;
            _inputOption = inputOption;
            _getEnterKey = getEnterKey;
        }

        public void SetPlayerMono(
            IMonoBehaviour playerMono
            , List<AbstractPlayerViewMono?> playerViewMonos
            )
        {
            _playerMono = playerMono;
            _attackExecutor.SetPlayerViewMonos(playerViewMonos);
        }

        public void Update(float deltaTime)
        {
            OpenMenuUpdate();

            Debug.Log($"_speechEventManager.GetSpeechEventEnum() = {_speechEventManager.GetSpeechEventEnum()}, CurrentEvent = {_speechEventManager.CurrentEvent}, Message = {_speechEventManager.CurrentEvent?.Speech}");

            if (_inputOption.IsOpening)
            {
                OptionUpdate();
            }
            else
            {
                switch (_speechEventManager.GetSpeechEventEnum())
                {
                    case SpeechEventEnum.Listening:
                        TutorialListeningUpdate();
                        break;
                    case SpeechEventEnum.Practical:
                        TutorialPracticalUpdate();
                        break;
                    case SpeechEventEnum.NoInput:
                        // 何もしない
                        break;
                    case SpeechEventEnum.None:
                        break;
                }
            }
        }

        void TutorialListeningUpdate()
        {
            if (_inputSerialManager.GetButtonRed())
            {
                _speechEventManager.MoveNext();
            }
        }

        void TutorialPracticalUpdate()
        {
            Debug.Log($"AttackExecutorTutorial: _speechEventManager.GetSpeechEventEnum() = {_speechEventManager.GetSpeechEventEnum()}" +
                      $", SpeechEventEnum.Message = {_speechEventManager.CurrentEvent?.Speech }");
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

            if (_getEnterKey.GetEnterKeyDown())
            {
                _daipanExecutor.DaiPan();
                _speechEventManager.MoveNext();
            }
        }
        void OpenMenuUpdate()
        {
            if (_inputSerialManager.GetButtonMenu())
            {
                if (!_inputOption.IsOpening) _inputOption.OpenOption();
                else _inputOption.CloseOption();
            }

        }
        void OptionUpdate()
        {

            if (_inputSerialManager.GetButtonRed()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Down);
            if (_inputSerialManager.GetButtonBlue()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Right);
            if (_inputSerialManager.GetButtonYellow()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Left);
            if (_getEnterKey.GetEnterKeyDown()) _inputOption.Select();
        }
    }
}