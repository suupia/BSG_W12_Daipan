#nullable enable
using Daipan.Core.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using DG.Tweening;
using Daipan.Player.Scripts;
using Daipan.Stream.Interfaces;
using R3;

namespace Daipan.Streamer.MonoScripts
{
    public class StreamerViewMono : MonoBehaviour, IUpdate
    {
        [SerializeField] Animator animator = null!;
        [SerializeField] Animator daipanEffect = null!;
        [SerializeField] Animator daipanWhiteEffect = null!;
        [SerializeField] Material daipanWaveMaterial = null!;
        [SerializeField] Material daipanDistortionMaterial = null!;
        [SerializeField] float daipanWaveSpeed;
        [SerializeField] float scaleRatio;
        [SerializeField] Vector3 moveAmountByAngerZoom;
        [SerializeField] float zoomDuration;

        IIrritatedGaugeValue _irritatedGaugeValue = null!;
        Vector3 _originalScale;
        Vector3 _originalPosition;
        Transform _transform = null!;
        float _effectDelaySec = 0.8f;

        [Inject]
        void Initialize(IIrritatedGaugeValue irritatedGaugeValue)
        {
            _irritatedGaugeValue = irritatedGaugeValue;

            _transform = transform;
            _originalPosition = transform.position;
            _originalScale = _transform.localScale;

            Observable.EveryValueChanged(irritatedGaugeValue, x => x.IsFull)
                .Subscribe(_ => AngerZoom(irritatedGaugeValue.IsFull))
                .AddTo(this);

            daipanWaveMaterial.SetFloat("_Radius", 0);
            daipanDistortionMaterial.SetFloat("_Radius", 0);
        }

        void IUpdate.Update()
        {
            animator.SetInteger("IrritatedStage", _irritatedGaugeValue.CurrentIrritatedStage);
        }

        public void Daipan()
        {
            animator.SetTrigger("IsDaipan");
            daipanEffect.SetTrigger("IsDaipan");
            daipanWhiteEffect.SetTrigger("IsDaipan");
            // 台パンのエフェクトを動かすところ
            DOVirtual.Float(0f, 2f, daipanWaveSpeed, value =>
            {
                daipanWaveMaterial.SetFloat("_Radius", value);
                daipanDistortionMaterial.SetFloat("_Radius", value);
            }).OnComplete(() =>
            {
                daipanWaveMaterial.SetFloat("_Radius", 0);
                daipanDistortionMaterial.SetFloat("_Radius", 0);
            });
        }

        public void AngerZoom(bool isFull)
        {
            Debug.Log($"AngerZoom isFull : {isFull}");
            
            var sequence = DOTween.Sequence();
            // 怒ってるとき拡大
            if (isFull)
            {
                sequence.Append(_transform.DOScale(_originalScale * scaleRatio, zoomDuration));
                sequence.Join(_transform.DOMove(_originalPosition + moveAmountByAngerZoom, zoomDuration));
                return;
            }


            // 怒ってないとき通常サイズに
            Observable.Timer(System.TimeSpan.FromSeconds(_effectDelaySec))
                .Subscribe(_ =>
                {
                    DOTween.Sequence()
                        .Append(_transform.DOScale(_originalScale, zoomDuration))
                        .Join(_transform.DOMove(_originalPosition, zoomDuration));
                });
        }
    }
}