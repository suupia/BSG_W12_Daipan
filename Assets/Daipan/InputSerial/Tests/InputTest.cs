using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Daipan.InputSerial.Scripts;

public class InputTest : MonoBehaviour
{
    void Update()
    {
        if (InputSerialManager.GetButtonBlue())
        {
            Debug.Log("Blue");
        }
        if (InputSerialManager.GetButtonRed())
        {
            Debug.Log("Red");
        }
        if (InputSerialManager.GetButtonGreen())
        {
            Debug.Log("Green");
        }
    }
}
