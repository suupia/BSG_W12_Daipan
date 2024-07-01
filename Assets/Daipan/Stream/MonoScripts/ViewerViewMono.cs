#nullable enable
using UnityEngine;
using VContainer;
using Daipan.Stream.Scripts;
using R3;
using TMPro;
using DG.Tweening;
using UnityEngine.Serialization;

namespace Daipan.Stream.MonoScripts
{
    public class ViewerViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI viewerText = null!;
        [FormerlySerializedAs("digitSplitterMono")] [SerializeField] DigitSplitViewMono digitSplitViewMono = null!;
        [SerializeField] int zoomingViewerThreshold;
        [SerializeField] float scaleRatio;
        [SerializeField] float zoomingDuration;

        Vector3 _originalScale;
        Transform _transform = null!;

        [Inject]
        public void Initialize(ViewerNumber viewerNumber)
        {
            _transform = viewerText.transform;
            _originalScale = _transform.localScale;

            Observable.EveryValueChanged(viewerNumber, x => viewerNumber.Number)
                .Subscribe(_ => UpdateViewerText(viewerNumber.Number))
                .AddTo(this);
        }

        void UpdateViewerText(int viewerNumber)
        {
            // var sequence = DOTween.Sequence();
            // if (viewerNumber >= ZoomingViewerThreshold)
            // {
            //     sequence.Append(_transform.DOScale(_originalScale * scaleRatio, zoomingDuration))
            //        .SetEase(Ease.OutBounce);
            // }
            // else
            // {
            //     sequence.Append(_transform.DOScale(_originalScale, zoomingDuration));
            // }
            viewerText.text = $"{viewerNumber}";
            digitSplitViewMono.SetDigit(viewerNumber);
        }
    }
}