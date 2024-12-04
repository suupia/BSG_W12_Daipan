#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Daipan.Result.MonoScripts
{
    public class ResultMonoOnline : MonoBehaviour
    {
        [SerializeField] TMP_Text resultText = null!;

        void Start()
        {
            resultText.text = NetworkPlayerResultHolder.NetworkPlayerResultEnum.ToString();
        }
    }

    public static class NetworkPlayerResultHolder
    {
        public static NetworkPlayerResultEnum NetworkPlayerResultEnum { get; set; } = NetworkPlayerResultEnum.Quit;
    }

    public enum NetworkPlayerResultEnum
    {
        Win,
        Lose,
        Quit,
    }
}