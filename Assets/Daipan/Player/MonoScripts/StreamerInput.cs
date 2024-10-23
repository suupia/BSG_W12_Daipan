#nullable enable

using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Core.Interfaces;
using Daipan.InputSerial.Interfaces;
using Daipan.InputSerial.Scripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using Daipan.Transporter;
using Daipan.Transporter.Scripts;
using Fusion;
using UnityEngine;
using VContainer;

public class StreamerInput : IPlayerInput
{
    readonly NetworkRunner _runner;
    readonly IInputSerialManager _inputSerialManager;
    readonly IAttackExecutor _attackExecutor;
    readonly DaipanExecutor _daipanExecutor;
    readonly ResultState _resultState;
    readonly EndSceneSelector _endSceneSelector;
    readonly IInputOption _inputOption;
    readonly IGetEnterKey _getEnterKey;
    readonly PlayerDataTransporterNetWrapper _playerDataTransporterNetWrapper;
    readonly bool _isStreamer;
    IMonoBehaviour? _playerMono;

    public StreamerInput(
        NetworkRunner runner,
        IInputSerialManager inputSerialManager,
        IAttackExecutor attackExecutor,
        DaipanExecutor daipanExecutor,
        ResultState resultState,
        EndSceneSelector endSceneSelector,
        IInputOption inputOption,
        IGetEnterKey getEnterKey,
        PlayerDataTransporterNetWrapper playerDataTransporterNetWrapper
    )
    {
        _runner = runner;
        _inputSerialManager = inputSerialManager;
        _attackExecutor = attackExecutor;
        _daipanExecutor = daipanExecutor;
        _resultState = resultState;
        _endSceneSelector = endSceneSelector;
        _inputOption = inputOption;
        _getEnterKey = getEnterKey;
        _playerDataTransporterNetWrapper = playerDataTransporterNetWrapper;
        _isStreamer = _playerDataTransporterNetWrapper.GetPlayerRoleEnum(runner.LocalPlayer) == PlayerRoleEnum.Streamer;
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
        if (!_isStreamer) return;
        
        Debug.Log($"_serialManager.GetButtonRed() = {_inputSerialManager.GetButtonRed()}");
        if (_resultState.CurrentResultEnum != ResultState.ResultEnum.None)
        {
            ResultUpdate();
        }
        else
        {
            OpenMenuUpdate();
    
            if (_inputOption.IsOpening)
                OptionUpdate();
            else
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
    
        if (_getEnterKey.GetEnterKeyDown()) _daipanExecutor.DaiPan();
    }
    
    void ResultUpdate()
    {
        switch (_resultState.CurrentResultEnum)
        {
            case ResultState.ResultEnum.Result:
                if (_getEnterKey.GetEnterKeyDown())
                {
                    Debug.Log("Result中でEnterが押されたよ");
                    _resultState.ShowDetails();
                }
    
                break;
            case ResultState.ResultEnum.Details:
                if (_getEnterKey.GetEnterKeyDown())
                {
                    Debug.Log("Result中でEnterが押されたよ");
                    _endSceneSelector.TransitToEndScene();
                }
    
                break;
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