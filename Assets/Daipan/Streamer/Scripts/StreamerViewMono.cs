#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using DG.Tweening;
using Daipan.Player.Scripts;
using R3;

namespace Daipan.Streamer.Scripts
{
    public class StreamerViewMono : MonoBehaviour
    {
        [SerializeField] Animator animator = null!;
        [SerializeField] float scaleRatio;
        [SerializeField] Vector3 moveAmountByAngerZoom;
        [SerializeField] float zoomDuration;

        IrritatedValue _irritatedValue = null!;
        Vector3 _originalScale;
        Vector3 _originalPosition;
        Transform _transform;

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
            Debug.Log($"CurrentIrritatedStage : {_irritatedValue.CurrentIrritatedStage} Ratio : {_irritatedValue.Ratio}");

        }

        public void Daipan()
        {
            animator.SetBool("IsDaipan", true);
        }

        public void AngerZoom(bool isFull)
        {
            var senquence = DOTween.Sequence();
            // 怒ってるとき拡大
            if (isFull)
            {
                senquence.Append(_transform.DOScale(_originalScale * scaleRatio, zoomDuration));
                senquence.Join(_transform.DOMove(_originalPosition + moveAmountByAngerZoom, zoomDuration));
                return;
            }

            // 怒ってないとき通常サイズに
            senquence.Append(_transform.DOScale(_originalScale, zoomDuration));
            senquence.Join(_transform.DOMove(_originalPosition, zoomDuration));

        }
    }
}
