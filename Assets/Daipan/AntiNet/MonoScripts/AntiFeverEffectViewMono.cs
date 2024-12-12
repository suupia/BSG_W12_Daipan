#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Net;
using UnityEngine.UI;
using DG.Tweening;
using System;
using Daipan.Sound.MonoScripts;

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiFeverEffectViewMono : MonoBehaviour
    {
        [SerializeField] GameObject particleEffect = null!;
        [SerializeField] Animator particleAnimation = null!;
        [SerializeField] float fadeTime = 0.5f;
        private Sequence? _sequence = null!;
        private IDisposable? _disposable = null!;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            particleEffect.SetActive(false);
            Observable
                .EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    if (value == AntiStateEnum.FEVER)
                    {
                        ShowEffect();
                    }
                    else
                    {
                        HideEffect();
                    }
                })
                .AddTo(this);


        }

        void ShowEffect()
        {
            particleEffect.SetActive(true);
            particleEffect.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            particleAnimation.SetTrigger("Fire");
        }

        void HideEffect()
        {
            _sequence = DOTween.Sequence();

            _sequence.Append(
                DOVirtual.Float(
                    1f,
                    0f,
                    fadeTime,
                    onVirtualUpdate: (value) =>
                    {
                        particleEffect.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, value);
                    })
                    .SetEase(Ease.Linear)
                    .OnComplete(() => particleEffect.gameObject.SetActive(false))
            );
        }
    }
}