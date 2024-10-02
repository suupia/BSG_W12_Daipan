#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using DG.Tweening;
using Daipan.Player.Scripts;
using R3;

namespace Daipan.Streamer.MonoScripts
{
    public class StreamerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;
        [SerializeField] Animator daipanEffect = null!;
        [SerializeField] Animator daipanWhiteEffect = null!;
        [SerializeField] Material daipanWaveMaterial = null!;
        [SerializeField] float scaleRatio;
        [SerializeField] Vector3 moveAmountByAngerZoom;
        [SerializeField] float zoomDuration;

        IrritatedValue _irritatedValue = null!;
        Vector3 _originalScale;
        Vector3 _originalPosition;
        Transform _transform = null!;
        float _effectDelaySec = 0.8f;

        [Inject]
        void Initialize(IrritatedValue irritatedValue)
        {
            _irritatedValue = irritatedValue;

            _transform = transform;
            _originalPosition = transform.position;
            _originalScale = _transform.localScale;

            Observable.EveryValueChanged(irritatedValue, x => x.IsFull)
                .Subscribe(_ => AngerZoom(irritatedValue.IsFull))
                .AddTo(this);
        }

        void Update()
        {
            animator.SetInteger("IrritatedStage", _irritatedValue.CurrentIrritatedStage);
        }

        public void Daipan()
        {
            animator.SetTrigger("IsDaipan");
            daipanEffect.SetTrigger("IsDaipan");
            daipanWhiteEffect.SetTrigger("IsDaipan");
            // 台パンのエフェクトを動かすところ
            DOVirtual.Float(0f, 2f, 1f, value =>
            {
                daipanWaveMaterial.SetFloat("_Radius", value);
                Debug.Log($"radius : {value}");
            });
        }

        public void AngerZoom(bool isFull)
        {
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