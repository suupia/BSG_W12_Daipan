#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

namespace Daipan.Effects.MonoScripts
{
    public class CommentEffectMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer frontLight = null!;
        [SerializeField] SpriteRenderer backLight = null!;
        [SerializeField] Transform star = null!;

        [SerializeField] Vector2 distinatian;

        [SerializeField] float glowingTime;
        [SerializeField] float dimmingTime;
        [SerializeField] float moveTime;
        [SerializeField] float waveBand;

        Action? _onDead;


        public void Initialize(Action? onDead)
        {
            _onDead = onDead;
            Vector3 direction = new Vector3(distinatian.x, distinatian.y, 0f) - star.position;
            float a = UnityEngine.Random.Range(-waveBand, waveBand);

            var sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.Float(0, 1f, glowingTime, value =>
            {
                frontLight.color = new Vector4(1f, 1f, 1f, value);
                backLight.color = new Vector4(1f, 1f, 1f, value);
            }).OnComplete(() => {
                star.gameObject.SetActive(true);
                DOVirtual.Float(0, 1f, moveTime, value =>
                {
                    star.localPosition = direction * value + Vector3.up * Mathf.Sin(Mathf.PI * value) * a;
                    star.eulerAngles = new Vector3(0f, 0f, 500f * value);

                }).SetEase(Ease.Linear).OnComplete(() =>
                {
                    if (_onDead != null) _onDead();
                    Destroy(gameObject);
                });
            }
            ));

            sequence.Append(DOVirtual.Float(1f, 0, dimmingTime, value =>
            {
                frontLight.color = new Vector4(1f, 1f, 1f, value);
                backLight.color = new Vector4(1f, 1f, 1f, value);
            }).OnComplete(() => frontLight.gameObject.SetActive(false)
            ));
        }
    }
}