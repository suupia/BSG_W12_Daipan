#nullable enable
using System;
using Daipan.Player.Scripts;
using TMPro;
using UnityEngine;
using VContainer;
using R3;
using DG.Tweening;

namespace Daipan.Player.MonoScripts
{
    public sealed class ComboInstantViewMono : MonoBehaviour
    {
        [SerializeField] GameObject viewObject = null!;
        [SerializeField] TextMeshProUGUI comboText = null!;

        [SerializeField] float scaleRatio;
        [SerializeField] float fadeoutDuration;
        [SerializeField] float scaleUpDuration;
        [SerializeField] float scaleDownDuration;


        Vector3 _originalScale;

        void Awake()
        {
            Hide();
        }

        public void ShowComboText(int comboCount)
        {
            // comboCountが0なら表示しない
            if (comboCount <= 0) Destroy(gameObject);
            
            // 他のComboInstantViewMonoを削除
            var comboInstantViewMonos = FindObjectsOfType<ComboInstantViewMono>();
            foreach (var comboInstantViewMono in comboInstantViewMonos)
            {
                if (comboInstantViewMono != this) Destroy(comboInstantViewMono.gameObject);
            }

            // 初期のスケールが設定されていない場合は、現在のスケールを設定
            if (_originalScale == Vector3.zero) _originalScale = viewObject.transform.localScale;

            comboText.text = $"{comboCount}";
            Show();

            // 初期スケールに設定してからアニメーションを開始
            viewObject.transform.localScale = _originalScale;

            // DOTweenのシーケンスを作成
            var sequence = DOTween.Sequence();

            // 拡大アニメーションを追加
            sequence.Append(viewObject.transform.DOScale(_originalScale * scaleRatio, scaleUpDuration)
                .SetEase(Ease.InOutCubic));

            // 縮小アニメーションを追加し、拡大後に実行
            sequence.Append(viewObject.transform.DOScale(_originalScale, scaleDownDuration)
                .SetEase(Ease.InOutCubic));

            // アニメーション完了後にオブジェクトを削除
            sequence.OnComplete(() => Destroy(gameObject));
        }

        void Hide()

        {
            comboText.gameObject.SetActive(false);
        }

        void Show()
        {
            comboText.gameObject.SetActive(true);
        }
    }
}