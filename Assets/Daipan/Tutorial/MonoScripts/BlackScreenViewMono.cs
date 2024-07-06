#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Daipan.Tutorial.MonoScripts
{
    public class BlackScreenViewMono : MonoBehaviour
    {
        [SerializeField] Image blackScreenImage = null!;
       
        public void FadeOut(float time, Action onComplete)
        {
            blackScreenImage.DOFade(0, time).OnComplete(() =>
            {
                onComplete();
            });
        }

        public void Hide()
        {
            blackScreenImage.gameObject.SetActive(false);
        }
    } 
}

