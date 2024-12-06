#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using Daipan.Battle.scripts;

namespace Daipan.Result.MonoScripts
{
    public class ResultMonoOnline : MonoBehaviour
    {
        [SerializeField] TMP_Text resultText = null!;

        void Start()
        {
            resultText.text = NetworkPlayerResultHolder.NetworkPlayerResultEnum.ToString();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SceneTransition.TransitioningScene(SceneName.TitleSceneNet);
            }
        }
    }

    public static class NetworkPlayerResultHolder
    {
        public static NetworkPlayerResultEnum NetworkPlayerResultEnum { get; set; } = NetworkPlayerResultEnum.None;
}

    public enum NetworkPlayerResultEnum
    {
        None,
        Win,
        Lose,
        Quit,
    }
}