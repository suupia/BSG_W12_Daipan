#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Core.Interfaces;
using Daipan.Option.Interfaces;
using Daipan.InputSerial.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Option.Scripts {
    public class OptionInputUpdater : IUpdate
    {
        IInputOption _inputOption;
        InputSerialManager _inputSerialManager;
        
        [Inject]
        public OptionInputUpdater(IInputOption inputOption
            ,InputSerialManager inputSerialManager)
        {
            _inputOption = inputOption;
            _inputSerialManager = inputSerialManager;
        }
        
        public void Update()
        {
            if (_inputSerialManager.GetButtonMenu())
            {
                if (!_inputOption.IsOpening) _inputOption.OpenOption();
                else _inputOption.CloseOption();
            }

            if (_inputOption.IsOpening)
            {
                if (_inputSerialManager.GetButtonRed()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Down);
                if (_inputSerialManager.GetButtonBlue()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Right);
                if (_inputSerialManager.GetButtonYellow()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Left);
                if (Input.GetKeyDown(KeyCode.Return)) _inputOption.Select();
            }
        }
    }
}