#nullable enable
using System;
using UnityEngine;
using UnityEngine.UI;
using R3;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace Daipan.Effects.MonoScripts
{
    public class FrameViewMono : MonoBehaviour
    {
        [SerializeField] Image originalFrame;
        [SerializeField] Image antiFrame;
        [SerializeField] Image maxAntiFrame;

        [SerializeField] Transform antiCommentParent;

        [Header("アンチコメントがn以上あれば、antiFrame")]
        [Min(0)]
        [SerializeField] int antiThreshold;

        [Header("アンチコメントがn以上あれば、maxAntiFrame")]
        [Min(0)]
        [SerializeField] int maxAntiThreshold;

        [Header("アンチエフェクト")]
        [SerializeField] GameObject antiEffect;

        [Header("両端からn以下の位置にランダムにエフェクト生成(0~0.5)")]
        [Range(0,0.5f)]
        [SerializeField] float spawnWithin;

        [Min(0)]
        [SerializeField] float lifeTime;

        [Min(0.1f)]
        [SerializeField] float interval;

        int preAntiCommentNum = 0;
        IDisposable antiEffectSubscription;
        List<int> effectsY = new();

        void Start()
        {
            ShowMaxAntiFrame();
        }

        
        void Update()
        {
            if (antiThreshold >= maxAntiThreshold) Debug.LogError("antiThreshold >= maxAntiThreshold は不正値です。");
            int antiCommentNum = antiCommentParent.childCount;
            //Debug.Log($"antiCommentNum:{antiCommentNum}");

            if (preAntiCommentNum == antiCommentNum) return;


            if (antiCommentNum < antiThreshold)
            {
                //ShowOriginalFrame();
                ShowMaxAntiFrame();
            }
            else if(antiCommentNum < maxAntiThreshold)
            {
                ShowAntiFrame();
            }
            else
            {
                ShowMaxAntiFrame();
            }


            preAntiCommentNum = antiCommentNum;
        }

        void ShowOriginalFrame()
        {
            originalFrame.color = Color.white;
            antiFrame.enabled = false;
            maxAntiFrame.enabled = false;

            antiEffectSubscription?.Dispose();
        }
        void ShowAntiFrame()
        {
            originalFrame.color = new Vector4(0.8f, 0.8f, 0.8f, 1f);
            antiFrame.enabled = true;
            maxAntiFrame.enabled = false;

            antiEffectSubscription?.Dispose();
        }
        void ShowMaxAntiFrame()
        {
            originalFrame.color = new Vector4(0.8f, 0.8f, 0.8f, 1f);
            antiFrame.enabled = true;
            maxAntiFrame.enabled = true;

            antiEffectSubscription?.Dispose();
            antiEffectSubscription = Observable.Interval(TimeSpan.FromSeconds(interval), destroyCancellationToken)
            .Subscribe(_ =>
            {
                if (effectsY.Count >= lifeTime / interval) effectsY.RemoveAt(0);

                Vector3 screenPos = new Vector3(UnityEngine.Random.Range(0, spawnWithin * 2), 0, 0);
                if (screenPos.x >= spawnWithin) screenPos.x = 1 - screenPos.x * 0.5f;

                // Y座標決め
                int num = (int)MathF.Ceiling(lifeTime / interval);
                List<int> numbers = new();
                for (int i = 0; i < num; i++)
                {
                    if (!effectsY.Contains(i))
                        numbers.Add(i);
                }
                screenPos.y = numbers[UnityEngine.Random.Range(0, numbers.Count)] + UnityEngine.Random.value;
                screenPos.y /= num;

                effectsY.Add((int)MathF.Floor(screenPos.y * num));

                var pos = Camera.main.ViewportToWorldPoint(screenPos);
                pos.z = 0;


                var effect = Instantiate(antiEffect, pos, Quaternion.identity);
                effect.transform.localScale = effect.transform.localScale * UnityEngine.Random.Range(0.5f, 0.7f);


                var sequence = DOTween.Sequence();
                //effect.GetComponent<SpriteRenderer>().color.a = 0f;
                sequence.Append(DOVirtual.Float(0, 0.7f, lifeTime * 0.5f, value =>
                {
                    effect.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, value);
                }).SetLoops(1, LoopType.Yoyo));

                sequence.Join(effect.transform.DOScale(effect.transform.localScale * 1.5f, lifeTime * 0.5f));

                sequence.Join(effect.transform.DORotate(new Vector3(0f, 0f, -10f), lifeTime * 0.2f).SetLoops(5, LoopType.Yoyo));


                sequence.Append(DOVirtual.Float(0.7f, 0f, lifeTime * 0.5f, value =>
                {
                    effect.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, value);
                }));

                //sequence.Join(effect.transform.DORotate(new Vector3(0f, 0f, 270), lifeTime * 0.3f, RotateMode.FastBeyond360));

                Destroy(effect, lifeTime);
            });
        }

    }
}