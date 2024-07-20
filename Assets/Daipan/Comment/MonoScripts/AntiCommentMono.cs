#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
using DG.Tweening;
using UnityEngine;
using VContainer;
using TMPro;

namespace Daipan.Comment.MonoScripts
{
    public sealed class AntiCommentMono : MonoBehaviour
    {
        [SerializeField] TextMeshPro commentText = null!;

        AntiCommentCluster _antiCommentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;
        IrritatedValue _irritatedValue = null!; 
        
        [Inject]
        public void Initialize(
            AntiCommentCluster antiCommentCluster
            , CommentParamsServer commentParamsServer
            , IrritatedValue irritatedValue
        )
        {
            _antiCommentCluster = antiCommentCluster;
            _commentParamsServer = commentParamsServer;
            _irritatedValue = irritatedValue;
        }

        void Update()
        {
            _irritatedValue.IncreaseValue(  _commentParamsServer.GetIrritationIncreasePerSec() * Time.deltaTime);
        }

        public void SetParameter(string commentWord)
        {
            commentText.text = commentWord;
        }

        public void Daipaned()
        {
            const float rotationDuration = 0.4f;
            
            // ここで潰れる演出をいれる。
            // commentText.transform
            //     .DOScaleY(0, 0.2f) // 小さくする
            //     .SetEase(Ease.InQuint)
            //     .DOTimeScale(1.1f, 0.15f) // 素早く大きくする
            //     .SetEase(Ease.InQuint)
            //     .Join
            //     // .SetEase(Ease.OutBounce)
            //     .OnComplete(() =>
            //     {
            //         _antiCommentCluster.Remove(this);
            //         Destroy(gameObject);
            //     });

            Sample();
        }
        void Sample()
        {
            var sequence = DOTween.Sequence()
                .Append(commentText.transform.DOScaleY(0, 0.2f).SetEase(Ease.InQuint)) // 小さくする
                .Append(commentText.transform.DOScaleY(1.1f, 0.15f).SetEase(Ease.InQuint)) // 素早く大きくする
                // .Join(commentText.transform.DOMoveY(1, 0.2f).SetEase(Ease.InQuint) )  // 同時に上に移動
                // .Append(commentText.transform.DOScaleY(0, 0.2f).SetEase(Ease.InQuint)) // 小さくしながら
                // .Join(commentText.transform.DOMoveY(-1, 0.2f).SetEase(Ease.InQuint) )  // 同時に下に移動
                   .OnComplete(() =>
                    {
                        _antiCommentCluster.Remove(this);
                        Destroy(gameObject);
                    });
            

            sequence.Play();
        }


    } 
}

