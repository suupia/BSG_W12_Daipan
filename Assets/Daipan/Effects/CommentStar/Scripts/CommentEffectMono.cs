using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

namespace Daipan.Effects.MonoScripts
{
    public class CommentEffectMono : MonoBehaviour
    {
        [SerializeField]
        SpriteRenderer light;
        [SerializeField]
        Transform star;

        [SerializeField]
        Vector2 distinatian;

        [SerializeField]
        float glowingTime;
        [SerializeField]
        float dimmingTime;
        [SerializeField]
        float moveTime;

        private void Start()
        {
            Vector3 direction = new Vector3(distinatian.x, distinatian.y, 0f) - star.position;

            var sequence = DOTween.Sequence();
            sequence.Append(DOVirtual.Float(0, 1f, glowingTime, value =>
            {
                light.color = new Vector4(1f, 1f, 1f, value);
            }).OnComplete(() => {
                star.gameObject.SetActive(true);
                DOVirtual.Float(0, 1f, moveTime, value =>
                {
                    star.localPosition = direction * value + Vector3.up * Mathf.Sin(Mathf.PI * value);
                    star.eulerAngles = new Vector3(0f, 0f, 1000f * value);
                }).SetEase(Ease.Linear);
            }
            ));

            sequence.Append(DOVirtual.Float(1f, 0, dimmingTime, value =>
            {
                light.color = new Vector4(1f, 1f, 1f, value);
            }).OnComplete(() => light.gameObject.SetActive(false)
            ));

            

        }
    }
}