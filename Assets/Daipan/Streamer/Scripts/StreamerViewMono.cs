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

        IrritatedValue _irritatedValue = null!;
        Vector3 _originalScale;
        Transform _transform;

        [Inject]
        void Initialize(IrritatedValue irritatedValue)
        {
            _irritatedValue = irritatedValue;

            _transform = transform;
            _originalScale = _transform.localScale;



            Observable.EveryValueChanged(irritatedValue, x => x.IsFull)
                .Subscribe(_ => AngerZoom(irritatedValue.IsFull))
                .AddTo(this);
        }

        void Update()
        {
            animator.SetInteger("IrritatedStage", _irritatedValue.CurrentIrritatedStage);
            //Debug.Log($"CurrentIrritatedStage : {_irritatedValue.CurrentIrritatedStage}");

        }

        public void Daipan()
        {
            animator.SetBool("IsDaipan", true);
        }

        public void AngerZoom(bool isFull)
        {
            // 怒ってるとき拡大
            if (isFull)
            {
                _transform.DOScale(_originalScale * scaleRatio, 0.1f);
                return;
            }

            // 怒ってないとき通常サイズに
            _transform.DOScale(_originalScale, 0.1f);
            
        }
    }
}
