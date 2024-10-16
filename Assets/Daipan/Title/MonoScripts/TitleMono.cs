#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Enemy.Scripts;
using Daipan.Sound.MonoScripts;
using Daipan.Option.Interfaces;
using Daipan.Option.Scripts;
using UnityEngine;
using VContainer;
using Daipan.InputSerial.Scripts;
using Daipan.Core.Interfaces;
using UnityEngine.UI;
using DG.Tweening;

public class TitleMono : MonoBehaviour
{
    [SerializeField] Image blackScreen = null!;

    InputSerialManager _inputSerialManager = null!;
    IInputOption _inputOption = null!;
    IGetEnterKey _getEnterKey = null!;
    SamePressChecker? _samePressChecker;

    [Inject]
    public void Initialize(
        InputSerialManager inputSerialManager,
        IInputOption inputOption,
        IGetEnterKey getEnterKey
        )
    {
        Time.timeScale = 1; // timeScaleを戻す
        
        var soundManager = FindObjectOfType<SoundManager>();
        if (soundManager == null)
            Debug.LogWarning("SoundManager is not found");
        else
           soundManager.Initialize();
        
        SoundManager.Instance?.PlayBgm(BgmEnum.Title);
        Debug.Log("TitleMono Initialized");

        _inputSerialManager = inputSerialManager;
        _inputOption = inputOption;
        _samePressChecker = new SamePressChecker(0.5f, 3, () => SceneTransition.TransitioningScene(SceneName.DaipanScene), () => { });

        _getEnterKey = getEnterKey;
    }


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
                return; // todo : 仮
                const float fadeoutTime = 0.3f;
                blackScreen.gameObject.SetActive(true);
                DOVirtual.Float(0, 1f, fadeoutTime, value =>
                {
                    blackScreen.color = new Vector4(0, 0, 0, value);
                }).OnComplete(() => SceneTransition.TransitioningScene(SceneName.TutorialScene));
                SoundManager.Instance?.PlaySe(SeEnum.Decide);
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                SceneTransition.TransitioningScene(SceneName.DaipanScene);
            }
            
            // 3つのボタンを同時に推したらチュートリアルをスキップする
            if(_inputSerialManager.GetButtonRed()) _samePressChecker?.SetOn(0);
            if(_inputSerialManager.GetButtonBlue()) _samePressChecker?.SetOn(1);
            if(_inputSerialManager.GetButtonYellow()) _samePressChecker?.SetOn(2);
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
        if (_inputSerialManager.GetButtonBlue()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Right);
        if (_inputSerialManager.GetButtonYellow()) _inputOption.MoveCursor(MoveCursorDirectionEnum.Left);
        if (_getEnterKey.GetEnterKeyDown()) _inputOption.Select();
    }
}
