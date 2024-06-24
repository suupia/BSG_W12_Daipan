#nullable enable
using Daipan.Player.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using DG.Tweening;

namespace Daipan.Player.MonoScripts
{
    public class ComboViewMono : MonoBehaviour
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
            var senquence = DOTween.Sequence();

            // comboCountが0ならフェードアウト
            if (comboCount == 0)
            {
                senquence.Append(_transform.DOScale(Vector3.zero, fadeoutDuration));
                return;
            }

            // 増える時にアニメーション
            // 拡大
            senquence.Append(
                _transform.DOScale(_originalScale * scaleRatio, scaleUpDuration)
                .SetEase(Ease.InOutCubic));


            // 縮小
            senquence.Append(
                _transform.DOScale(_originalScale, scaleDownDuration)
                .SetEase(Ease.InOutCubic)
                .SetDelay(scaleUpDuration));

            comboText.text = $"{comboCount}";
        }
    }
}