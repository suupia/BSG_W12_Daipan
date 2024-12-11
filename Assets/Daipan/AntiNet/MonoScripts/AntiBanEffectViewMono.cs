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
        [SerializeField] GameObject banEffect = null!;
        [SerializeField] Animator banEffectAnimator = null!;
        // animationの終了関係なくuncontrollableTimeで終了
        [SerializeField] float uncontrollableTime;

        private Sequence _sequence = null!;
        private IDisposable? _disposable = null!;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            banEffect.SetActive(false);
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
            banEffect.SetActive(true);
            banEffectAnimator.SetTrigger("Fire");

            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(uncontrollableTime))
                .Subscribe(_ => banEffect.SetActive(false));
        }
    }
}