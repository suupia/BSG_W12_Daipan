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
    public class AntiBanEffectViewMono : MonoBehaviour
    {
        [SerializeField] GameObject banChainEffect = null!;
        [SerializeField] Animator banChainEffectAnimator = null!;
        // animationの終了関係なくuncontrollableTimeで終了
        [SerializeField] float uncontrollableTime;

        [SerializeField] Image banMessage = null!;
        [SerializeField] TMP_Text banMessageText = null!;
        [SerializeField] float blinkingTime;

        private Sequence? _sequence = null!;
        private IDisposable? _disposable = null!;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            banChainEffect.SetActive(false);
            banMessage.gameObject.SetActive(false);
            Observable
                .EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    if (value == AntiStateEnum.BAN)
                    {
                        ShowEffect();
                        ShowMessage(antiStateValue.IsDaipaned);
                    }
                    else
                    {
                        HideMessage();
                    }
                })
                .AddTo(this);


        }

        void ShowEffect()
        {
            _disposable?.Dispose();
            banChainEffect.SetActive(true);
            SoundManager.Instance?.PlaySe(SeEnum.BAN);
            banChainEffectAnimator.SetTrigger("Fire");

            _disposable = Observable
                .Timer(TimeSpan.FromSeconds(uncontrollableTime))
                .Subscribe(_ => banChainEffect.SetActive(false));
        }

        void ShowMessage(bool isDaipaned)
        {
            _sequence = DOTween.Sequence();

            banMessage.gameObject.SetActive(true);
            banMessageText.text = isDaipaned
                ? "配信者ちゃんにBANされた！？"
                : "アンチコメント書き過ぎて \n モデレーターにBANされた！";
            banMessageText.fontSize = isDaipaned
                ? 90 : 65;

            _sequence.Append(
                DOVirtual.Float(
                    0.5f,
                    1f,
                    blinkingTime,
                    onVirtualUpdate: (value) =>
                    {
                        banMessageText.color = new Color(0.906f, 0.271f, 0.243f, value);
                    })
                    .SetEase(Ease.InOutQuart)
                    .SetLoops(-1, LoopType.Yoyo)
            );
        }

        void HideMessage()
        {
            banMessage.gameObject.SetActive(false);
            _sequence?.Kill();
        }
    }
}