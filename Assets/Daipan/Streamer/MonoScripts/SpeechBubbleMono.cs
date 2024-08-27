#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Sound.MonoScripts;
using Daipan.Tutorial.MonoScripts;
using Daipan.Tutorial.Scripts;
using TMPro;
using UnityEngine;
using DG.Tweening;
using R3;
using UnityEditor.VersionControl;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using UnityEngine.UI;
using VContainer;

namespace Daipan.Streamer.MonoScripts
{
    public class SpeechBubbleMono : MonoBehaviour
    {
        // 画像付きの吹き出し
        [SerializeField] Image speechImage = null!;

        [SerializeField] TextMeshProUGUI speechWithSpriteText = null!;

        // 画像なしの吹き出し
        [SerializeField] TextMeshProUGUI speechText = null!;

        const float DurationSec = 0.5f;
        const double MinShowSec = 0.5;
        readonly Queue<Speech> _speechQueue = new();
        TutorialSpeechSpritesMono _speechSprites = null!;
        public bool IsStartTutorial { get; set; } // Loadingの時に吹き出しが出ないようにするために必要

        double Timer { get; set; }

        [Inject]
        public void Construct(SpeechEventManager speechEventManager)
        {
            Observable.EveryValueChanged(speechEventManager, x => x.CurrentEvent)
                .Subscribe(_ => EnqueueSpeechMessage(speechEventManager.CurrentEvent.Speech))
                .AddTo(this);
        }

        void Awake()
        {
            // 初期状態は非表示
            transform.localScale = Vector3.zero;
            _speechSprites = FindObjectOfType<TutorialSpeechSpritesMono>();
            Assert.IsNotNull(_speechSprites);
        }

        void Update()
        {
            if (!IsStartTutorial) return;

            Timer += Time.deltaTime;

            if (_speechQueue.Any())
                if (Timer > MinShowSec)
                {
                    ShowSpeechBubble(_speechQueue.Dequeue());
                    Timer = 0;
                }
        }

        void ShowSpeechBubble(Speech speech)
        {
            // DoTweenでDoScaleで拡大して表示
            transform.DOScale(Vector3.one, DurationSec).OnComplete(() =>
            {
                if (speech.SpriteKey != string.Empty)
                {
                    speechWithSpriteText.text = speech.Message;
                    speechImage.sprite = _speechSprites.GetSprite(speech.SpriteKey);
                    speechText.text = string.Empty;
                }
                else
                {
                    speechWithSpriteText.text = string.Empty;
                    speechImage.sprite = null; 
                    speechText.text = speech.Message;
                }

                if (speech.Message != string.Empty) SoundManager.Instance?.PlaySe(SeEnum.Text);
            });
        }

        void EnqueueSpeechMessage(Speech message)
        {
            if (_speechQueue.LastOrDefault() == message) return;
            _speechQueue.Enqueue(message);
        }

        public void HideSpeechBubble()
        {
            // DoTweenでDoScaleで縮小して非表示
            transform.DOScale(Vector3.zero, DurationSec);
        }
    }
}