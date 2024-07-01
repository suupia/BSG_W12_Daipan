#nullable enable
using UnityEngine;
using VContainer;
using DG.Tweening;

namespace Daipan.Stream.MonoScripts
{
    public class ShakeDisplayMono : MonoBehaviour
    {
        [Header("揺らしたいオブジェクト")]
        [SerializeField] GameObject shakedObject;
        
        [SerializeField] float duration;
        [SerializeField] float shakePower;
        [SerializeField] int shakeNum;

        Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = shakedObject.transform.position;
        }

        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Backspace)) Daipan();
#endif
        }

        public void Daipan()
        {
            shakedObject.transform.DOShakePosition(duration, shakePower, shakeNum, 1, false, true)
                .OnComplete(() =>
                {
                    shakedObject.transform.position = _originalPosition;
                });
        }
    }
}