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

namespace Daipan.AntiNet.MonoScripts
{
    public class AntiCommentInputViewMono : MonoBehaviour
    {
        [SerializeField] Image writingImage = null!;
        [SerializeField] Image banImage = null!;
        [SerializeField] List<TMP_Text> characterTexts = null!;
        [SerializeField] float duration;

        private Sequence _sequence = null!;

        [Inject]
        public void Initialize(AntiStateValue antiStateValue)
        {
            Observable
                .EveryValueChanged(antiStateValue, x => x.AntiStateEnum)
                .Subscribe(value =>
                {
                    if (value == AntiStateEnum.BAN)
                    {
                        banImage.gameObject.SetActive(true);
                    }
                    else
                    {
                        banImage.gameObject.SetActive(false);
                    }
                })
                .AddTo(this);
        }

        void Start()
        {
            var transform = writingImage.gameObject.GetComponent<RectTransform>();
            transform.localScale = new Vector3(0.5f, 0f, 1f);
            transform.anchoredPosition = new Vector2(703f, -29.7f);

            _sequence = DOTween.Sequence();
        }
        public void UpdateCharacters(string characters)
        {
            for (int i = 0; i < characterTexts.Count; i++)
            {
                if (i >= characters.Length) characterTexts[i].text = "";
                else characterTexts[i].text = $"{characters[i]}";
            }
        }
        public void OpenChat()
        {
            writingImage.gameObject.SetActive(true);


            _sequence.Kill();
            _sequence = DOTween.Sequence();
            var transform = writingImage.gameObject.GetComponent<RectTransform>();
            transform.localScale = new Vector3(0.5f, 0f, 1f);
            _sequence.Append(transform.DOScale(Vector3.one, duration).SetEase(Ease.OutQuad));

            transform.anchoredPosition = new Vector2(703f, -29.7f);
            _sequence.Join(transform.DOAnchorPos(new Vector2(569.5f, -29.7f), duration).SetEase(Ease.OutQuad));
        }
        public void CloseChat()
        {
            _sequence.Kill();
            _sequence = DOTween.Sequence();
            var transform = writingImage.gameObject.GetComponent<RectTransform>();
            transform.localScale = Vector3.one;
            transform.DOScale(new Vector3(0.5f, 0f, 1f), duration).SetEase(Ease.OutQuad).OnComplete(() => writingImage.gameObject.SetActive(false));

            transform.anchoredPosition = new Vector2(569.5f, -29.7f);
            _sequence.Join(transform.DOAnchorPos(new Vector2(703f, -29.7f), duration).SetEase(Ease.OutQuad));
        }
    }
}