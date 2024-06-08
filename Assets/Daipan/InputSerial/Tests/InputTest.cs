using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.InputSerial.Scripts;
using VContainer;

public class InputTest : MonoBehaviour
{
    InputSerialManager _inputSerialManager;

    [Inject]
    InputTest(InputSerialManager inputSerialManager)
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
        if (_inputSerialManager.GetButtonGreen())
        {
            Debug.Log("Green");
        }
    }
}
