#nullable enable
using UnityEngine;
using VContainer;
using DG.Tweening;

namespace Daipan.Stream.MonoScripts
{
    public class ShakeDisplayMono : MonoBehaviour
    {
        [SerializeField] float duration;
        [SerializeField] float shakePower;
        [SerializeField] int ShakeNum;

        Vector3 _originalPosition;

        private void Start()
        {
            _originalPosition = Camera.main.transform.position;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backspace)) Daipan();
        }

        public void Daipan()
        {
            Camera.main.transform.DOShakePosition(duration, shakePower, ShakeNum, 1, false, true)
                .OnComplete(() =>
                {
                    transform.position = _originalPosition;
                });
        }
    }
}