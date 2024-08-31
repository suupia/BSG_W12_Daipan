using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class IrritatedGaugeSpotLightMono : MonoBehaviour
{
    [SerializeField] GameObject shadowObject = null!;
    [SerializeField] RectTransform unmaskRect = null!;
    
    void Awake()
    {
        shadowObject.SetActive(false);
    }

    public void Show()
    {
        shadowObject.SetActive(true);
        // unmaskRectのサイズを0から2に変化させるアニメーションをDoTweenで実装
        unmaskRect.localScale = Vector3.zero; // 初期サイズを0に設定
        unmaskRect.DOScale(Vector3.one * 2, 1f); // 1秒かけてスケールを2に変化させる
    }
    
    public void Hide()
    {
        // unmaskRectのサイズを2から無限大に変化させるアニメーションをDoTweenで実装
        unmaskRect.DOScale(Vector3.one * 100, 0.8f)
            .OnComplete(() => shadowObject.SetActive(false)); // アニメーションが終了したら非表示にする
        
        
    }
}