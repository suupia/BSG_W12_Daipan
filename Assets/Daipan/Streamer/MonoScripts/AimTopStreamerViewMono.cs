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
    public class AimTopStreamerViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;

        void Awake()
        {
            viewObject.SetActive(false);
        }

        public void Show()
        {
            // なしになった。
            // aimTopStreamerText.text = $"目指せ！神配信者！";
            // viewObject.SetActive(true);
        }

        public void Hide()
        {
            viewObject.SetActive(false);
        }
    }
}