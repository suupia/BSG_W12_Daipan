using System.Collections;
using System.Collections.Generic;
using Daipan.Player.Scripts;
using UnityEngine;
using VContainer;

public class ComboTestMono : MonoBehaviour
{
    ComboCounter _comboCounter;

    [Inject]
    public void Initialize(ComboCounter comboCounter)
    {
        _comboCounter = comboCounter;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _comboCounter.IncreaseCombo();
        if (Input.GetKeyDown(KeyCode.RightShift)) _comboCounter.IncreaseCombo();
    }
}
