using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Daipan.Battle.scripts
{
    public static class SceneTransition
    {
        static readonly Dictionary<SceneName, string> SceneNameTable = new()
        {
            { SceneName.TitleScene, "TitleScene" },
            { SceneName.TutorialScene, "TutorialScene" },
            { SceneName.DaipanScene, "DaipanScene" },
            { SceneName.EndScene, "EndScene" }
        };

        public static void TransitioningScene(SceneName nextScene)
        {
            if (SceneNameTable.TryGetValue(nextScene, out var sceneName))
            {
                Debug.Log($"Transitioning to {sceneName}");
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError($"{nextScene} is not registered in sceneNameTable.");
            }
        }
    }

    public enum SceneName
    {
        TitleScene,
        TutorialScene,
        DaipanScene,
        EndScene
    }
}