#nullable enable

using System;
using UnityEditor;
using UnityEngine;

public class MenuButtonsMono : MonoBehaviour
{
    [SerializeField] CustomButton onlineButton = null!;
    [SerializeField] CustomButton offlineButton = null!;
    [SerializeField] CustomButton optionButton = null!;
    [SerializeField] CustomButton quitButton = null!;

    void Awake()
    {
        var titleMonoNew = FindObjectOfType<TitleMonoNew>();
        if(titleMonoNew == null)
        {
            Debug.LogError($"TitleMonoNew is null");
            return;
        }
        
        onlineButton.OnClick += () => titleMonoNew.GoToJoinPanel(); 
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