using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using UnityEngine;

public class ResultMono : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneTransition.TransitioningScene(SceneName.TitleScene);
        }
    }
}
