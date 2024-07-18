#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Sound.MonoScripts;
using Daipan.Tutorial.Scripts;
using TMPro;
using UnityEngine;
using DG.Tweening;
using R3;
using VContainer;

namespace Daipan.Streamer.MonoScripts
{
    public class SpeechBubbleMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI speechText = null!;

        const float DurationSec = 0.5f;
        double Timer { get; set; }
        const double MinShowSec = 0.5; 
        readonly Queue<string> _speechQueue = new ();
        
        public bool IsStartTutorial { get; set; } // Loadingの時に吹き出しが出ないようにするために必要

        [Inject]
        public void Construct(SpeechEventManager speechEventManager)
        {
            Observable.EveryValueChanged(speechEventManager, x => x.CurrentEvent)
                .Subscribe(_ => EnqueueSpeechMessage(speechEventManager.CurrentEvent.Message))
                .AddTo(this);
        }
        
        void Awake()
        {
            // 初期状態は非表示
            transform.localScale = Vector3.zero; 

        }
        
        void Update()
        {
            if (!IsStartTutorial) return;
            
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
                SoundManager.Instance?.PlaySe(SeEnum.Text);
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