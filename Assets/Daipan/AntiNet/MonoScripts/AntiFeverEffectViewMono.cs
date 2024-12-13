#nullable enable
using System;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Net;
using UnityEngine.UI;
using DG.Tweening;
using Daipan.Sound.MonoScripts;

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiFeverEffectViewMono : MonoBehaviour
    {
        [SerializeField] GameObject particleEffect = null!;
        [SerializeField] Animator particleAnimation = null!;
        [SerializeField] GameObject becameFeverEffect = null!;
        [SerializeField] float fadeTime = 0.5f;
        private Sequence? _sequence = null!;
        private IDisposable? _disposable = null!;

        // SE再生フラグ
        private bool _hasPlayedFeverSe = false;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            particleEffect.SetActive(false);
            becameFeverEffect.gameObject.SetActive(false);

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
            becameFeverEffect.gameObject.SetActive(true);

            // 初回のみSEを再生
            if (!_hasPlayedFeverSe)
            {
                SoundManager.Instance?.PlaySe(SeEnum.FeverTime);
                _hasPlayedFeverSe = true;
            }
        }

        void HideEffect()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(
                DOVirtual.Float(
                    1f,
                    0f,
                    fadeTime,
                    onVirtualUpdate: (value) =>
                    {
                        var spriteRenderer = particleEffect.GetComponent<SpriteRenderer>();
                        if (spriteRenderer != null)
                        {
                            spriteRenderer.color = new Color(1f, 1f, 1f, value);
                        }
                    })
                    .SetEase(Ease.Linear)
                    .OnComplete(() =>
                    {
                        particleEffect.gameObject.SetActive(false);
                        becameFeverEffect.gameObject.SetActive(false);

                        // Hideしたのでフラグをリセット
                        _hasPlayedFeverSe = false;
                    }
                    )
            );
        }
    }
}
