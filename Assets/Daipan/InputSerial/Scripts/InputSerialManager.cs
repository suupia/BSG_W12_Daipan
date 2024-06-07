using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using System;
using System.Threading.Tasks;
using R3;

namespace Daipan.InputSerial.Scripts
{
    public static class InputSerialManager
    {
        static SerialInput _serialInput = null;
        static float _chatteringSec = 0.02f;
        static bool[] _isFirstInputs = new bool[30];
        static bool[] _isDuringPress = new bool[30];

        [Inject]
        public static void Initialize(SerialInput serialInput)
        {
            _serialInput = serialInput;
            for(int i = 0; i < _isFirstInputs.Length; i++)
            {
                _isFirstInputs[i] = true;
            }
            for (int i = 0; i < _isDuringPress.Length; i++)
            {
                _isDuringPress[i] = false;
            }
        }

        // キー入力
        public static bool GetButtonRed()
        {
            return getInput(0);
        }
        public static bool GetButtonGreen()
        {
            return getInput(1);
        }
        public static bool GetButtonBlue()
        {
            return getInput(2);
        }
        // ここまで



        static bool getInput(int digit)
        {
            // シリアルポートが有効か？
            if (!isSerial()) return false;

            // チャタリングチェック
            if (_isDuringPress[digit]) return true;

            // 受け取った入力がT/Fか？
            if ((_serialInput.number & 1 << digit) == 0)
            {
                _isFirstInputs[digit] = true;
                return false;
            }

            

            // 初めての入力か？つまりKeyDown
            if (!_isFirstInputs[digit]) return false;


            _isFirstInputs[digit] = false;

            // チャタリング開始
            _isDuringPress[digit] = true;
            Observable.Timer(TimeSpan.FromSeconds(_chatteringSec)).Subscribe(_ =>
                {
                    _isDuringPress[digit] = false;
                });

            return true;
        }
        static bool isSerial()
        {
            if (_serialInput == null) return false;
            if (!_serialInput.isSerial) return false;

            return true;
        }

    }
}
