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

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiBanEffectViewMono : MonoBehaviour
    {
        [SerializeField] GameObject banChainEffect = null!;
        [SerializeField] Animator banChainEffectAnimator = null!;
        // animationの終了関係なくuncontrollableTimeで終了
        [SerializeField] float uncontrollableTime;

        private Sequence _sequence = null!;
        private IDisposable? _disposable = null!;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            banChainEffect.SetActive(false);
            Observable
                .EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    if (value == AntiStateEnum.BAN)
                    {
                        ShowEffect();
                    }
                })
                .AddTo(this);
        }

        void ShowEffect()
        {
            _disposable?.Dispose();
            banChainEffect.SetActive(true);
            banChainEffectAnimator.SetTrigger("Fire");

            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(uncontrollableTime))
                .Subscribe(_ => banChainEffect.SetActive(false));
        }
    }
}