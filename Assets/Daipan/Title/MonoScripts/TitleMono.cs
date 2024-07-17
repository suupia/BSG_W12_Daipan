using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using Daipan.Sound.Interfaces;
using Daipan.Sound.MonoScripts;
using UnityEngine;
using VContainer;

public class TitleMono : MonoBehaviour
{
    [Inject]
    public void Initialize( ISoundManager soundManager)
    {
        soundManager.PlayBgm(BgmEnum.Title); 
        Debug.Log("TitleMono Initialized");
    }
    

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            SceneTransition.TransitioningScene(SceneName.TutorialScene);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SceneTransition.TransitioningScene(SceneName.DaipanScene); 
        }
    }
}
