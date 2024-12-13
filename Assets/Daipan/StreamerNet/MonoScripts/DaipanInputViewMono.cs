#nullable enable
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace Daipan.StreamerNet.MonoScripts
{
    public class DaipanInputViewMono : MonoBehaviour
    {
        [SerializeField] Image wordImage = null!;
        [SerializeField] Image headImage = null!;
        [SerializeField] Image handImage = null!;

        [SerializeField] Vector2 wordPosition;
        [SerializeField] Vector2 headPosition;
        [SerializeField] Vector2 handPosition;
        [SerializeField] Vector2 wordMove;
        [SerializeField] Vector2 headMove;
        [SerializeField] Vector2 handMove;
        [SerializeField] float duration;

        Sequence? _sequence = null!;

        // SE再生フラグ（Hideでリセット）
        bool _hasPlayedInitialWordSe;
        bool _hasPlayedInitialInputSe;

        [Inject]
        public void Initialize(
            IIrritatedGaugeValue irritatedGaugeValue
        )
        {
            wordImage.gameObject.SetActive(false);
            headImage.gameObject.SetActive(false);
            handImage.gameObject.SetActive(false);
            wordPosition = wordImage.GetComponent<RectTransform>().anchoredPosition;
            headPosition = headImage.GetComponent<RectTransform>().anchoredPosition;
            handPosition = handImage.GetComponent<RectTransform>().anchoredPosition;

            Observable.EveryValueChanged(irritatedGaugeValue, x => x.IsFull)
                .Subscribe(_ =>
                {
                    if (irritatedGaugeValue.IsFull)
                    {
                        ShowView();
                    }
                    else
                    {
                        HideView();
                    }
                })
                .AddTo(this);
            HideView();

        }

        void ShowView()
        {
            wordImage.gameObject.SetActive(true);
            headImage.gameObject.SetActive(true);
            handImage.gameObject.SetActive(true);

            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(
                wordImage.GetComponent<RectTransform>().DOAnchorPos(wordPosition, 0.5f)
                    .SetEase(Ease.Linear)
            )
            .Join(
                headImage.GetComponent<RectTransform>().DOAnchorPos(headPosition, 0.5f)
                    .SetEase(Ease.Linear)
            )
            .Join(
                handImage.GetComponent<RectTransform>().DOAnchorPos(handPosition, 0.5f)
                    .SetEase(Ease.Linear)
            )
            .OnComplete(() =>
            {
                AnimationView();
            });
        }

        void AnimationView()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence
                // Wordが降りてきた時に1回だけSE再生
                .AppendCallback(() =>
                {
                    if (!_hasPlayedInitialWordSe)
                    {
                        SoundManager.Instance?.PlaySe(SeEnum.DaipanWord);
                        _hasPlayedInitialWordSe = true;
                    }
                })

                // 前方向への移動アニメーション
                .Append(wordImage.GetComponent<RectTransform>()
                    .DOAnchorPos(wordPosition + wordMove, duration)
                    .SetEase(Ease.InQuart))
                .Join(headImage.GetComponent<RectTransform>()
                    .DOAnchorPos(headPosition + headMove, duration)
                    .SetEase(Ease.InQuart))
                .Join(handImage.GetComponent<RectTransform>()
                    .DOAnchorPos(handPosition + handMove, duration)
                    .SetEase(Ease.InQuart))

                // 元の位置へ戻るアニメーション
                .Append(wordImage.GetComponent<RectTransform>()
                    .DOAnchorPos(wordPosition, duration * 1.5f)
                    .SetEase(Ease.Linear))
                .Join(headImage.GetComponent<RectTransform>()
                    .DOAnchorPos(headPosition, duration * 1.5f)
                    .SetEase(Ease.Linear))
                .Join(handImage.GetComponent<RectTransform>()
                    .DOAnchorPos(handPosition, duration * 1.5f)
                    .SetEase(Ease.Linear))

                // 手が現れた(全アニメ復帰後)のタイミングで1回だけSE再生
                .AppendCallback(() =>
                {
                    if (!_hasPlayedInitialInputSe)
                    {
                        SoundManager.Instance?.PlaySe(SeEnum.DaipanInput);
                        _hasPlayedInitialInputSe = true;
                    }
                })

                // 繰り返し
                .SetLoops(-1, LoopType.Restart);
        }

        void HideView()
        {
            // Hide時にSE再生状態をリセット
            _hasPlayedInitialWordSe = false;
            _hasPlayedInitialInputSe = false;

            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence.Append(
                wordImage.GetComponent<RectTransform>().DOAnchorPos(wordPosition + new Vector2(0, 2000f), 1.5f)
                    .SetEase(Ease.Linear)
            )
            .Join(
                headImage.GetComponent<RectTransform>().DOAnchorPos(headPosition + new Vector2(0, 2000f), 1.5f)
                    .SetEase(Ease.Linear)
            )
            .Join(
                handImage.GetComponent<RectTransform>().DOAnchorPos(handPosition + new Vector2(-2000, 0), 1.5f)
                    .SetEase(Ease.Linear)
            )
            .OnComplete(() =>
            {
                wordImage.gameObject.SetActive(false);
                headImage.gameObject.SetActive(false);
                handImage.gameObject.SetActive(false);
            });
        }

    }
}
