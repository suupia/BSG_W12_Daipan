#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Daipan.Tutorial.MonoScripts
{
    public class BlackScreenViewMono : MonoBehaviour
    {
        [SerializeField] CanvasGroup canvasGroup = null!;
        [SerializeField] GameObject animesprite=null!;
        [SerializeField] GameObject blackscreenview = null!;
        public void FadeIn(float time, Action onComplete)
        {
            blackscreenview.SetActive(false);
            animesprite.SetActive(true);
            canvasGroup.DOFade(1, time).OnComplete(() =>
            {
                onComplete();
            });
        }
       
        public void FadeOut(float time, Action onComplete)
        {
            canvasGroup.DOFade(0, time).OnComplete(() =>
            {
                onComplete();
            });
        }
    } 
}

