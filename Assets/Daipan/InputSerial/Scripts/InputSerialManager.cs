using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;


namespace Daipan.InputSerial.Scripts
{
    public static class InputSerialManager
    {
        static SerialInput _serialInput = null;
        [Inject]
        public static void Initialize(SerialInput serialInput)
        {
            _serialInput = serialInput;
        }

        // キー入力
        public static bool GetButtonRed()
        {
            if (!isSerial()) return false;

            if ((_serialInput.number & 1) == 1) return true;

            return false;
        }
        public static bool GetButtonGreen()
        {
            if (!isSerial()) return false;

            if ((_serialInput.number & 2) == 1) return true;

            return false;
        }
        public static bool GetButtonBlue()
        {
            if (!isSerial()) return false;

            if ((_serialInput.number & 4) == 1) return true;

            return false;
        }
        // ここまで


        static bool isSerial()
        {
            if (_serialInput == null) return false;
            if (!_serialInput.isSerial) return false;

            return true;
        }

    }
}
