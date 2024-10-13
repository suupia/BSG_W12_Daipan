#nullable enable

using System;
using UnityEditor;
using UnityEngine;

public class TitleButtonsMono : MonoBehaviour
{
    [SerializeField] CustomButton onlineButton = null!;
    [SerializeField] CustomButton offlineButton = null!;
    [SerializeField] CustomButton optionButton = null!;
    [SerializeField] CustomButton quitButton = null!;

    void Awake()
    {
        var selectOnlineGameModeMono = FindObjectOfType<SelectOnlineGameModeMono>();
        if(selectOnlineGameModeMono == null)
        {
            Debug.LogError($"SelectOnlineGameModeMono is null");
            return;
        }
        
        onlineButton.OnClick += () => selectOnlineGameModeMono.ShowPopup(); 
        offlineButton.OnClick += () => Debug.Log($"実装するか未定");
        offlineButton.OnClick += () => Debug.Log($"実装お願いします");
        quitButton.OnClick += Quit;
    }
    
    void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false; //ゲームプレイ終了
#else
        Application.Quit();//ゲームプレイ終了
#endif
    }
    
}