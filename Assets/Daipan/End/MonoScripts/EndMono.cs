using System.Collections;
using System.Collections.Generic;
using Daipan.Battle.scripts;
using UnityEngine;

namespace Daipan.End.MonoScripts
{
    public class EndMono : MonoBehaviour
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
 
}
