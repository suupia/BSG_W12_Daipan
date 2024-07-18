#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
using UnityEngine;
using VContainer;
using Daipan.InputSerial.Scripts;
using Daipan.Core.Interfaces;

public class TitleMono : MonoBehaviour
{
    InputSerialManager _inputSerialManager;
    IInputOption _inputOption;
    SamePressChecker _samePressChecker = null!;
    IGetEnterKey _getEnterKey;

    [Inject]
    public void Initialize(ISoundManager soundManager,
        InputSerialManager inputSerialManager,
        IInputOption inputOption,
        IGetEnterKey getEnterKey)
    {
        soundManager.PlayBgm(BgmEnum.Title);
        Debug.Log("TitleMono Initialized");

        _inputSerialManager = inputSerialManager;
        _inputOption = inputOption;
        _samePressChecker = new SamePressChecker(0.5f, 3,
            () => SceneTransition.TransitioningScene(SceneName.DaipanScene), () => { });

        _getEnterKey = getEnterKey;
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
            if (_getEnterKey.GetEnterKeyDown())
            {
                SceneTransition.TransitioningScene(SceneName.TutorialScene);
                SoundManager.Instance.PlaySe(SeEnum.Decide);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                SceneTransition.TransitioningScene(SceneName.DaipanScene);
            }
            
            // 3つのボタンを同時に推したらチュートリアルをスキップする
            if(_inputSerialManager.GetButtonRed()) _samePressChecker.SetOn(0);
            if(_inputSerialManager.GetButtonBlue()) _samePressChecker.SetOn(1);
            if(_inputSerialManager.GetButtonYellow()) _samePressChecker.SetOn(2);
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
        if (_getEnterKey.GetEnterKeyDown()) _inputOption.Select();
    }
}
