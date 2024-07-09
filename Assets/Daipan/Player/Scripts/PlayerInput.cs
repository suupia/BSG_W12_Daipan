#nullable enable
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerInput : IPlayerInput
    {
        readonly InputSerialManager _inputSerialManager;
        readonly IAttackExecutor _attackExecutor;
        readonly DaipanExecutor _daipanExecutor;
        readonly ResultState _resultState;
        readonly EndSceneSelector _endSceneSelector;
        PlayerMono? _playerMono;

        public PlayerInput(
            InputSerialManager inputSerialManager
            , IAttackExecutor attackExecutor
            , DaipanExecutor daipanExecutor
            , ResultState resultState
            , EndSceneSelector endSceneSelector
        )
        {
            _inputSerialManager = inputSerialManager;
            _attackExecutor = attackExecutor;
            _daipanExecutor = daipanExecutor;
            _resultState = resultState;
            _endSceneSelector = endSceneSelector;
        }

        public void SetPlayerMono(
            PlayerMono playerMono
            , List<AbstractPlayerViewMono?> playerViewMonos
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
            if (_resultState.IsInResult)
            {
                ResultUpdate();
            }
            else
            {
                PlayingUpdate();
            }
        }

        void PlayingUpdate()
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

            if (Input.GetKeyDown(KeyCode.Return)) _daipanExecutor.DaiPan();
        }

        void ResultUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Result中でEnterが押されたよ");
                _endSceneSelector.TransitToEndScene();
            }
        }
    }
}