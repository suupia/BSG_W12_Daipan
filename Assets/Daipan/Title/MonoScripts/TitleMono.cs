using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using UnityEngine;

public class TitleMono : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
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
