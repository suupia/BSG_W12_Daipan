#nullable enable
using System;
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

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
        public void Initialize(ViewerNumber viewerNumber)
        {
            PreViewerNumber = viewerNumber.Number;

            Observable
                .EveryValueChanged(viewerNumber, x => viewerNumber.Number)
                .Subscribe(newViewerNumber =>
                {
                    _tweener?.Kill();  // 以前のアニメーションを停止

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