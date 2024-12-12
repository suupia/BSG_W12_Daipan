#nullable enable
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using DG.Tweening;
using ExitGames.Client.Photon.StructWrapping;
using R3;
using UnityEngine;
using UnityEngine.PlayerLoop;
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



        private Sequence? _sequence = null!;

        [SerializeField] bool testFlag;
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
            ).OnComplete(() =>
            {
                AnimationView();
            });
        }
        void AnimationView()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            _sequence
                .Append(
                    wordImage.GetComponent<RectTransform>().DOAnchorPos(wordPosition + wordMove, duration)
                    .SetEase(Ease.InQuart)
                )
                .Join(
                    headImage.GetComponent<RectTransform>().DOAnchorPos(headPosition + headMove, duration)
                    .SetEase(Ease.InQuart)
                )
                .Join(
                    handImage.GetComponent<RectTransform>().DOAnchorPos(handPosition + handMove, duration)
                    .SetEase(Ease.InQuart)
                )
                .Append(
                    wordImage.GetComponent<RectTransform>().DOAnchorPos(wordPosition, duration * 1.5f)
                    .SetEase(Ease.Linear)
                )
                .Join(
                    headImage.GetComponent<RectTransform>().DOAnchorPos(headPosition, duration * 1.5f)
                    .SetEase(Ease.Linear)
                )
                .Join(
                    handImage.GetComponent<RectTransform>().DOAnchorPos(handPosition, duration * 1.5f)
                    .SetEase(Ease.Linear)
                )
                .SetLoops(-1, LoopType.Restart);
        }
        void HideView()
        {
            _sequence?.Kill();
            _sequence.Append(
                wordImage.GetComponent<RectTransform>().DOAnchorPos(wordPosition + new Vector2(0, 2000f), 0.5f)
                .SetEase(Ease.Linear)
            )
            .Join(
                headImage.GetComponent<RectTransform>().DOAnchorPos(headPosition + new Vector2(0, 2000f), 0.5f)
                .SetEase(Ease.Linear)
            )
            .Join(
                handImage.GetComponent<RectTransform>().DOAnchorPos(handPosition + new Vector2(-2000, 0), 0.5f)
                .SetEase(Ease.Linear)
            ).OnComplete(() =>
            {
                wordImage.gameObject.SetActive(false);
                headImage.gameObject.SetActive(false);
                handImage.gameObject.SetActive(false);
            });
        }

    }
}