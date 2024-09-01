#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Daipan.Tutorial.MonoScripts
{
    public class FadeInViewMono : MonoBehaviour
    {
        [SerializeField]
        Image blackScreen = null!;
        [SerializeField]
        float fadeInTime;

        private void Start()
        {
            DOVirtual.Float(1.2f, 0, fadeInTime, value =>
            {
                blackScreen.color = new Vector4(0, 0, 0, value);
            }).SetEase(Ease.InSine);
        }
    }
}