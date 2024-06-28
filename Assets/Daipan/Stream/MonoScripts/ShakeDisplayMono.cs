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


        public void Daipan()
        {
            Camera.main.transform.DOShakePosition(duration, shakePower, ShakeNum, 1, false, true);
        }
    }
}