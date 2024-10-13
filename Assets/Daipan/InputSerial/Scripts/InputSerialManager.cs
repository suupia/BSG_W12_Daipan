using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using System;
using System.Threading.Tasks;
using R3;

namespace Daipan.InputSerial.Scripts
{
    public class InputSerialManager
    {
        SerialInput _serialInput = null;
        float _chatteringSec = 0.02f;
        bool[] _isFirstInputs = new bool[30];
        bool[] _isDuringPress = new bool[30];

        bool[] _isPressing = new bool[30];



        [Inject]
        public void Initialize(SerialInput serialInput)
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
            for (int i = 0; i < _isPressing.Length; i++)
            {
                _isPressing[i] = false;
            }
        }

        // キー入力
        public bool GetButtonRed()
        {
            return getInput(0)
                || Input.GetKeyDown(KeyCode.W);
        }
        public bool GetButtonBlue()
        {
            return getInput(1)
                || Input.GetKeyDown(KeyCode.S);
        }
        public bool GetButtonYellow()
        {
            return getInput(2)
                || Input.GetKeyDown(KeyCode.A);
        }
        public bool GetButtonMenu()
        {
            return getInput(3)
                || Input.GetKeyDown(KeyCode.Tab);
        }
        public bool GetButtonAny()
        {
            return GetButtonRed() || GetButtonBlue() || GetButtonYellow();
        }

        //// ここまで
        //public bool GetButtonPressingRed()
        //{
        //    return getInputContinue(0)
        //        || Input.GetKey(KeyCode.W);
        //}
        //public bool GetButtonPressingBlue()
        //{
        //    return getInputContinue(1)
        //        || Input.GetKey(KeyCode.S);
        //}
        //public bool GetButtonPressingYellow()
        //{
        //    return getInputContinue(2)
        //        || Input.GetKey(KeyCode.A);
        //}
        //public bool GetButtonPressingMenu()
        //{
        //    return getInputContinue(3)
        //        || Input.GetKey(KeyCode.Tab);
        //}


        bool getInput(int digit)
        {
            // Debug.Log("GetInput : 0"); // 一旦コメントアウト
            // シリアルポートが有効か？
            if (!isSerial()) return false;
            Debug.Log("GetInput : 1");
            // チャタリングチェック
            if (_isDuringPress[digit]) return false;
            Debug.Log("GetInput : 2");
            // 受け取った入力がT/Fか？
            if ((_serialInput.number & 1 << digit) == 0)
            {
                _isFirstInputs[digit] = true;
                return false;
            }

            Debug.Log("GetInput : 3");

            // 初めての入力か？つまりKeyDown
            if (!_isFirstInputs[digit]) return false;
            Debug.Log("GetInput : 4");

            _isFirstInputs[digit] = false;
            Debug.Log("GetInput : 5");
            // チャタリング開始
            _isDuringPress[digit] = true;
            Observable.Timer(TimeSpan.FromSeconds(_chatteringSec),UnityTimeProvider.UpdateRealtime).Subscribe(_ =>
                {
                    _isDuringPress[digit] = false;
                });
            Debug.Log("GetInput : 6");
            return true;
        }

        //// 重複する部分もあるが、バグが怖いので新しく書く
        //bool getInputContinue(int digit)
        //{
        //    // シリアルポートが有効か？
        //    if (!isSerial()) return false;

        //    // 押されている途中ならtrue
        //    if (_isPressing[digit]) return true;

        //    // 受け取った入力がT/Fか？
        //    if ((_serialInput.number & 1 << digit) == 0)
        //    {
        //        _isPressing[digit] = false;
        //        return false;
        //    }

        //    // 一度trueになったら_chatteringSecの間trueを返す
        //    _isPressing[digit] = true;
        //    Observable.Timer(TimeSpan.FromSeconds(_chatteringSec)).Subscribe(_ =>
        //    {
        //        _isPressing[digit] = false;
        //    });

        //    return true;
        //}
        
        bool isSerial()
        {
            if (_serialInput == null)
            {
                //Debug.LogWarning("_serialPortがnullです。");
                return false;
            }
            if (!_serialInput.isSerial)
            {
                //Debug.LogWarning("_serialPortが開かれていません。");
                return false;
            }
            return true;
        }

    }
}
