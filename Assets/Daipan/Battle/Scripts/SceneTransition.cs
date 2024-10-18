using System.Collections;
using System.Collections.Generic;
using Fusion;
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
            { SceneName.EndScene, "EndScene" },
            { SceneName.Lobby, "LobbyScene" },
            { SceneName.DaipanSceneNet, "DaipanSceneNet" }
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

        public static void TransitionSceneWithNetworkRunner(NetworkRunner runner, SceneName nextScene)
        {
            if (SceneNameTable.TryGetValue(nextScene, out var sceneName))
            {
                Debug.Log($"Transitioning to {sceneName}");
                runner.LoadScene(sceneName);
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
        EndScene,
        Lobby,
        DaipanSceneNet
    }
}