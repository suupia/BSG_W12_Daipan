using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
using UnityEngine;
using VContainer;
using Daipan.InputSerial.Scripts;

public class TitleMono : MonoBehaviour
{
    InputSerialManager _inputSerialManager;
    IInputOption _inputOption;

    [Inject]
    public void Initialize(ISoundManager soundManager,
        InputSerialManager inputSerialManager,
        IInputOption inputOption)
    {
        soundManager.PlayBgm(BgmEnum.Title);
        Debug.Log("TitleMono Initialized");

        _inputSerialManager = inputSerialManager;
        _inputOption = inputOption;
    }


    // Update is called once per frame
    void Update()
    {
        OpenMenuUpdate();

        if (_inputOption.IsOpening)
        {
            OptionUpdate();
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneTransition.TransitioningScene(SceneName.TutorialScene);
                SoundManager.Instance.PlaySe(SeEnum.Decide);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                SceneTransition.TransitioningScene(SceneName.DaipanScene);
            }
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
        if (_inputSerialManager.GetButtonBlue()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Left);
        if (_inputSerialManager.GetButtonYellow()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Right);
        if (Input.GetKeyDown(KeyCode.Return)) _inputOption.Select();
    }
}
