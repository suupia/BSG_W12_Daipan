using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.InputSerial.Scripts;
using VContainer;

public class InputTest : MonoBehaviour
{
    InputSerialManager _inputSerialManager = null;

    [Inject]
    public void Initialize(InputSerialManager inputSerialManager)
    {
        _inputSerialManager = inputSerialManager;
    }

    void Update()
    {
        if (_inputSerialManager.GetButtonBlue())
        {
            Debug.Log("Blue");
        }
        if (_inputSerialManager.GetButtonRed())
        {
            Debug.Log("Red");
        }
        if (_inputSerialManager.GetButtonYellow())
        {
            Debug.Log("Yellow");
        }
        if(_inputSerialManager.GetButtonMenu())
        {
            Debug.Log("Menu");
        }
    }
}
