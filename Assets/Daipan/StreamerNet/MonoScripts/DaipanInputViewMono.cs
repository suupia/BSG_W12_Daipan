#nullable enable
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using DG.Tweening;
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


        private Sequence? _sequence = null!;

        [SerializeField] bool testFlag;
        [Inject]
        public void Initialize(
            IIrritatedGaugeValue irritatedGaugeValue
        )
        {
            HideView();
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
        }

        void Update()
        {
            if (testFlag)
            {
                ShowView();
            }
            else
            {
                HideView();
            }
        }

        void ShowView()
        {
            wordImage.gameObject.SetActive(true);
            headImage.gameObject.SetActive(true);
            handImage.gameObject.SetActive(true);
            _sequence = DOTween.Sequence();



        }
        void HideView()
        {
            wordImage.gameObject.SetActive(false);
            headImage.gameObject.SetActive(false);
            handImage.gameObject.SetActive(false);

            _sequence?.Kill();
        }

    }
}