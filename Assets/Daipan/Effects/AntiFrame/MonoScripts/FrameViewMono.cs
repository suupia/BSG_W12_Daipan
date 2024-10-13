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
        [SerializeField] Image originalFrame = null!;
        [SerializeField] Image antiFrame = null!;
        [SerializeField] Image maxAntiFrame = null!;

        [SerializeField] Transform antiCommentParent = null!;

        [Header("アンチコメントがn以上あれば、antiFrame")]
        [Min(0)]
        [SerializeField] int antiThreshold;

        [Header("アンチコメントがn以上あれば、maxAntiFrame")]
        [Min(0)]
        [SerializeField] int maxAntiThreshold;

        [Header("アンチエフェクト")]
        [SerializeField] GameObject antiEffect = null!;

        [Header("両端からn以下の位置にランダムにエフェクト生成(0~0.5)")]
        [Range(0,0.5f)]
        [SerializeField] float spawnWithin;

        [Min(0)]
        [SerializeField] float lifeTime;

        [Min(0.1f)]
        [SerializeField] float interval;

        [SerializeField] float downLength;

        [SerializeField] float changeTime;

        int _preAntiCommentNum = 0;
        IDisposable? _antiEffectSubscription;
        List<int> _effectsY = new();
        int _antiState;
        

        void Start()
        {
            originalFrame.color = Color.white;
            antiFrame.color = Vector4.zero;
            maxAntiFrame.color = Vector4.zero;
            _antiState = 0;
        }

        
        void Update()
        {
            if (antiThreshold >= maxAntiThreshold) Debug.LogError("antiThreshold >= maxAntiThreshold は不正値です。");
            int antiCommentNum = antiCommentParent.childCount;
            //Debug.Log($"antiCommentNum:{antiCommentNum}");

            if (_preAntiCommentNum == antiCommentNum) return;


            if (antiCommentNum < antiThreshold && _antiState != 0)
            {
                ShowOriginalFrame();
                _antiState = 0;
            }
            else if(antiCommentNum >= antiThreshold && antiCommentNum < maxAntiThreshold && _antiState != 1)
            {
                ShowAntiFrame();
                _antiState = 1;
            }
            else if(antiCommentNum >= maxAntiThreshold && _antiState != 2)
            {
                ShowMaxAntiFrame();
                _antiState = 2;
            }


            _preAntiCommentNum = antiCommentNum;
        }

        void ShowOriginalFrame()
        {
            DOVirtual.Float(0, 1f, changeTime, value =>
            {
                originalFrame.color = new Vector4(0.8f + 0.2f * value, 0.8f + 0.2f * value, 0.8f + 0.2f * value, 1f);
                antiFrame.color = antiFrame.color * (1 - value);
                maxAntiFrame.color = maxAntiFrame.color * (1 - value);
            });


            _antiEffectSubscription?.Dispose();
        }
        void ShowAntiFrame()
        {
            DOVirtual.Float(1f, 0, changeTime, value =>
            {
                originalFrame.color = new Vector4(0.8f + 0.2f * value, 0.8f + 0.2f * value, 0.8f + 0.2f * value, 1f);
                antiFrame.color = Vector4.one * (1 - value);
                maxAntiFrame.color = Vector4.zero;
            });

            _antiEffectSubscription?.Dispose();
        }
        void ShowMaxAntiFrame()
        {
            DOVirtual.Float(1f, 0, changeTime, value =>
            {
                originalFrame.color = new Vector4(0.8f, 0.8f, 0.8f, 1f);
                antiFrame.color = Color.white;
                maxAntiFrame.color = Vector4.one * (1 - value);
            });

            _antiEffectSubscription?.Dispose();
            _antiEffectSubscription = Observable.Interval(TimeSpan.FromSeconds(interval), destroyCancellationToken)
            .Subscribe(_ =>
            {
                if (_effectsY.Count >= lifeTime / interval) _effectsY.RemoveAt(0);

                Vector3 screenPos = new Vector3(UnityEngine.Random.Range(0, spawnWithin * 2), 0, 0);
                if (screenPos.x >= spawnWithin) screenPos.x = 1 - screenPos.x * 0.5f;

                // Y座標決め
                int num = (int)MathF.Ceiling(lifeTime / interval);
                List<int> numbers = new();
                for (int i = 0; i < num; i++)
                {
                    if (!_effectsY.Contains(i))
                        numbers.Add(i);
                }
                screenPos.y = numbers[UnityEngine.Random.Range(0, numbers.Count)] + UnityEngine.Random.value;
                screenPos.y /= num;

                _effectsY.Add((int)MathF.Floor(screenPos.y * num));

                var pos = Camera.main.ViewportToWorldPoint(screenPos);
                pos.y += downLength * 0.5f;
                pos.z = 0;


                var effect = Instantiate(antiEffect, pos, Quaternion.identity);
                effect.transform.localScale = effect.transform.localScale * UnityEngine.Random.Range(0.5f, 0.7f);


                var sequence = DOTween.Sequence();
                //effect.GetComponent<SpriteRenderer>().color.a = 0f;
                sequence.Append(DOVirtual.Float(0, 1f, lifeTime * 0.5f, value =>
                {
                    effect.GetComponent<SpriteRenderer>().color = new Vector4(1f, 1f, 1f, value);
                }).SetLoops(2, LoopType.Yoyo));

                sequence.Join(effect.transform.DOScale(effect.transform.localScale * 1.5f, lifeTime * 0.5f));

                sequence.Join(effect.transform.DORotate(new Vector3(0f, 0f, -10f), lifeTime * 0.2f).SetLoops(5, LoopType.Yoyo));

                sequence.Join(effect.transform.DOMoveY(effect.transform.position.y - downLength, lifeTime).SetEase(Ease.Linear));


                Destroy(effect, lifeTime);
            });
        }

    }
}