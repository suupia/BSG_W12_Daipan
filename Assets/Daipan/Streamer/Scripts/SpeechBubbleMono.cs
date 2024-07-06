#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using DG.Tweening;

namespace Daipan.Streamer.Scripts
{
    public class SpeechBubbleMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI speechText = null!;

        const float DurationSec = 0.5f;
        double Timer = 0;
        const double MinShowSec = 1.0;
        readonly Queue<string> _speechQueue = new ();

        void Awake()
        {
            // 初期状態は非表示
            transform.localScale = Vector3.zero; 
        }
        
        void Update()
        {
            Timer += Time.deltaTime;

            if (_speechQueue.Any())
            {
                if (Timer > MinShowSec)
                {
                    ShowSpeechBubble(_speechQueue.Dequeue());
                    Timer = 0;
                }
            }
        }

         void ShowSpeechBubble(string text)
        {
            // DoTweenでDoScaleで拡大して表示
            transform.DOScale(Vector3.one, DurationSec).OnComplete(() =>
            {
                speechText.text = text;
            });
        }

        public void EnqueueSpeechMessage(string message)
        {
            if(_speechQueue.LastOrDefault() == message) return;
            _speechQueue.Enqueue(message);
        }
        public void HideSpeechBubble()
        {
            // DoTweenでDoScaleで縮小して非表示
            transform.DOScale(Vector3.zero, DurationSec);
        }
    }
}