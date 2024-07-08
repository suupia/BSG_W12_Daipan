#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Tutorial.Scripts;
using TMPro;
using UnityEngine;
using DG.Tweening;
using R3;
using UnityEngine.Serialization;
using VContainer;

namespace Daipan.Streamer.Scripts
{
    public class PushEnterTextViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] TextMeshProUGUI pushEnterText = null!;

        void Awake()
        {
            viewObject.SetActive(false); 
        }

        public void Show()
        {
            pushEnterText.text = $"PUSH ENTER";
            viewObject.SetActive(true);
        }

        public void Hide()
        {
            viewObject.SetActive(false);
        }
    }
}