using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Daipan.Battle.scripts
{
    public static class SceneTransition
    {
        static readonly Dictionary<SceneName, string> SeneNameTable = new()
        {

        {SceneName.TitleScene,"TitleScene" },
        {SceneName.DaipanScene,"DaipanScene" },
        {SceneName.ResultScene,"ResultScene" },

        };

        public static void TransitioningScene(SceneName nextScene)
        {
            if (SeneNameTable.TryGetValue(nextScene, out var sceneName))
            {
                Debug.Log($"Transitioning to {sceneName}");
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
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
        DaipanScene,
        ResultScene
    }
}