#nullable enable
using System;
using Daipan.Sound.MonoScripts;
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;
using Daipan.Stream.Interfaces;

namespace Daipan.Stream.MonoScripts
{
    public sealed class ViewerViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerText = null!;
        [FormerlySerializedAs("digitSplitterMono")] [SerializeField] DigitSplitViewMono digitSplitViewMono = null!;

        int PreViewerNumber { get; set; }
        Tweener? _tweener;
        const float AnimationDuration = 0.3f;

        [Inject]
        public void Initialize(IViewerNumber viewerNumber)
        {
            PreViewerNumber = viewerNumber.Number;

            Observable
                .EveryValueChanged(viewerNumber, x => x.Number)
                .Subscribe(newViewerNumber =>
                {
                    _tweener?.Kill();  // 以前のアニメーションを停止

                    // 視聴者数の増減をチェック
                    if (newViewerNumber > PreViewerNumber)
                    {
                        // 視聴者が増えた時
                        SoundManager.Instance?.PlaySe(SeEnum.CountUpViewer);
                    }
                    else if (newViewerNumber < PreViewerNumber)
                    {
                        // 視聴者が減った時
                        SoundManager.Instance?.PlaySe(SeEnum.CountDownViewer);
                    }

                    // DOTweenでアニメーションを設定
                    _tweener = DOTween.To(() => PreViewerNumber, x => PreViewerNumber = x, newViewerNumber, AnimationDuration)
                        .OnUpdate(() => UpdateViewerText(PreViewerNumber))
                        .OnComplete(() => UpdateViewerText(newViewerNumber));
                })
                .AddTo(this);
        }

        void UpdateViewerText(int viewerNumber)
        {
            viewerText.text = $"{viewerNumber}";
            digitSplitViewMono.SetDigit(viewerNumber);
        }
    }
}
