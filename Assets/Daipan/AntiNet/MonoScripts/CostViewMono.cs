#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.AntiNet.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using DG.Tweening;

namespace Daipan.AntiNet.MonoScripts
{
    public class CostViewMono : MonoBehaviour
    {
        [SerializeField] TMP_Text costText = null!;
        [SerializeField] TMP_Text maxCostText = null!;
        [SerializeField] float duration;
        [SerializeField] float scale;

        private Sequence _sequence = null!;
        [Inject]
        public void Initialize(
            SpawnEnemyCostValue cost
            , ICostValueParam costParam)
        {
            _sequence = DOTween.Sequence();
            Observable
                .EveryValueChanged(cost, x => x.Value)
                .Subscribe(value =>
                {
                    UpdateText(value.ToString());
                })
                .AddTo(this);

            maxCostText.text = $"/ {costParam.MaxCostValue}";
        }

        void UpdateText(string text)
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();

            var transform = costText.gameObject.GetComponent<RectTransform>();
            transform.localScale = Vector3.one;

            _sequence.Append(transform.DOScale(Vector3.one * scale, duration / 2f).SetEase(Ease.OutQuad))
                // .AppendCallback(() => costText.text = text)
                .Append(transform.DOScale(Vector3.one, duration / 2f).SetEase(Ease.InQuad));
            costText.text = text;
        }
    }
}