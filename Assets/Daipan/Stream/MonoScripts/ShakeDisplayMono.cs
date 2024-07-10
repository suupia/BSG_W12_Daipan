#nullable enable
using UnityEngine;
using Daipan.Option.Scripts;
using VContainer;
using DG.Tweening;

namespace Daipan.Stream.MonoScripts
{
    public sealed class ShakeDisplayMono : MonoBehaviour
    {
        [Header("揺らしたいオブジェクト")]
        [SerializeField] GameObject shakedObject = null!;
        
        [SerializeField] float duration;
        [SerializeField] float shakePower;
        [SerializeField] int shakeNum;

        Vector3 _originalPosition;
        Quaternion _originalRotation;

        DaipanShakingConfig _daipanShakingConfig = null!;

        [Inject]
        public void Initialize(DaipanShakingConfig daipanShakingConfig)
        {
            _daipanShakingConfig = daipanShakingConfig;
        }

        void Start()
        {
            _originalPosition = shakedObject.transform.position;
            _originalRotation = shakedObject.transform.rotation;
        }

        void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Backspace)) Daipan();
#endif
        }

        public void Daipan()
        {
            if (!_daipanShakingConfig.IsShaking) return;

            var senquence = DOTween.Sequence();
            senquence.Append(shakedObject.transform.DOShakePosition(duration, shakePower, shakeNum, 1, false, true)
                .OnComplete(() =>
                {
                    shakedObject.transform.position = _originalPosition;
                }));
            senquence.Join(shakedObject.transform.DOPunchRotation(new Vector3(0, 0, 1f), duration, shakeNum, 1f)
                .OnComplete(() =>
                {
                    shakedObject.transform.rotation = _originalRotation;
                }));
        }
    }
}