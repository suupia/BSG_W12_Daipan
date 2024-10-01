#nullable enable
using Daipan.Player.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using DG.Tweening;

namespace Daipan.Player.MonoScripts
{
    public sealed class ComboViewMono : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI comboText = null!;

        [SerializeField] float scaleRatio;
        [SerializeField] float fadeoutDuration;
        [SerializeField] float scaleUpDuration;
        [SerializeField] float scaleDownDuration;


        Vector3 _originalScale;
        Transform _transform = null!;

        [Inject]
        public void Initialize(ComboCounter comboCounter)
        {
            _transform = comboText.transform;
            _originalScale = _transform.localScale;

            Observable.EveryValueChanged(comboCounter, x => x.ComboCount)
                .Subscribe(_ => UpdateComboText(comboCounter.ComboCount))
                .AddTo(this);
        }

        void UpdateComboText(int comboCount)
        {
            DOTween.Kill(_transform);

            // comboCountが0ならフェードアウト
            if(comboCount == 0)
            {
                _transform.DOScale(Vector3.zero, fadeoutDuration);
                return;
            }

            // 増える時にアニメーション
            // 拡大
            _transform.DOScale(_originalScale * scaleRatio, scaleUpDuration)
                .SetEase(Ease.InOutCubic);
                

            // 縮小
            _transform.DOScale(_originalScale, scaleDownDuration)
                .SetEase(Ease.InOutCubic)
                .SetDelay(scaleUpDuration);

            comboText.text = $"{comboCount}";
        }
    }
}