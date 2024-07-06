#nullable enable
using System;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Daipan.Streamer.Scripts
{
    public class SpeechBubbleMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI speechText = null!;

        const float DurationSec = 0.5f;

        void Awake()
        {
            // 初期状態は非表示
            transform.localScale = Vector3.zero; 
        }

        public void ShowSpeechBubble(string text)
        {
            // DoTweenでDoScaleで拡大して表示
            transform.DOScale(Vector3.one, DurationSec).OnComplete(() => { speechText.text = text; });
        }
        
        public void HideSpeechBubble()
        {
            // DoTweenでDoScaleで縮小して非表示
            transform.DOScale(Vector3.zero, DurationSec);
        }
    }
}