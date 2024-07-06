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

        {SceneName.TitleScene,"TitleScene" },
        {SceneName.TutorialScene,"TutorialScene" },
        {SceneName.DaipanScene,"DaipanScene" },
        {SceneName.ResultScene,"ResultScene" },
        {SceneName.InsideTheBox,"InsideTheBox"},
        {SceneName.Thanksgiving,"Thanksgiving" },
        {SceneName.BottomYoutuber,"BottomYoutuber" },
        {SceneName.TopYoutuber,"TopYoutuber" },
        {SceneName.ProGamer,"ProGamer" },
        {SceneName.SacredLady,"SacredLady" },
        {SceneName.Flame,"Flame" },
        {SceneName.Ordinary1,"Ordinary1" },
        {SceneName.Ordinary2,"Ordinary2" },
        {SceneName.Ordinary3,"Ordinary3" },
        };

        public static void TransitioningScene(SceneName nextScene)
        {
            if (SceneNameTable.TryGetValue(nextScene, out var sceneName))
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
        TutorialScene,
        DaipanScene,
        ResultScene,
        
        // End Scene
        InsideTheBox,
        Thanksgiving,
        BottomYoutuber,
        TopYoutuber,
        ProGamer,
        SacredLady,
        Flame,
        Ordinary1,
        Ordinary2,
        Ordinary3,
    }
}