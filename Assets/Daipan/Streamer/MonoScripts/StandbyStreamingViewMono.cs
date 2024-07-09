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

namespace Daipan.Streamer.MonoScripts
{
    public class StandbyStreamingViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] TextMeshProUGUI standbyText = null!;

        void Awake()
        {
            viewObject.SetActive(false); 
        }

        public void Show()
        {
            standbyText.text = $"配信待機場所"; 
            viewObject.SetActive(true);
        }

        public void Hide()
        {
            viewObject.SetActive(false);
        }
    }
}