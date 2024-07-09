using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using Daipan.Option.Interfaces;

namespace Daipan.Option.Tests
{
    public class OptionTestInputMono : MonoBehaviour
    {
        IInputOption _inputOption;
        [Inject]
        public void Init(IInputOption inputOption)
        {
            _inputOption = inputOption;
        }
        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!_inputOption.IsOpening) _inputOption.OpenOption();
                else _inputOption.CloseOption();
            }

            if (_inputOption.IsOpening)
            {
                if (Input.GetKeyDown(KeyCode.W)) _inputOption.MoveCursor(Scripts.MoveCursorDirectionEnum.Down);
                if (Input.GetKeyDown(KeyCode.A)) _inputOption.Select();
            }
        }
    }
}