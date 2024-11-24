#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
using DG.Tweening;
using UnityEngine;
using VContainer;
using TMPro;
using Daipan.Comment.Interfaces;
using Daipan.Stream.Interfaces;

namespace Daipan.Comment.MonoScripts
{
    public sealed class AntiCommentMono : MonoBehaviour, IAntiCommentMono
    {
        [SerializeField] TextMeshPro commentText = null!;

        AntiCommentCluster _antiCommentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;
        IIrritatedGaugeValue _irritatedGaugeValueNetwork = null!;
        bool IsActive { get; set; } = true;

        [Inject]
        public void Initialize(
            AntiCommentCluster antiCommentCluster
            , CommentParamsServer commentParamsServer
            , IIrritatedGaugeValue irritatedGaugeValueNetwork
        )
        {
            _antiCommentCluster = antiCommentCluster;
            _commentParamsServer = commentParamsServer;
            _irritatedGaugeValueNetwork = irritatedGaugeValueNetwork;
        }

        void Update()
        {
            if (IsActive) _irritatedGaugeValueNetwork.IncreaseValue(_commentParamsServer.GetIrritationIncreasePerSec() * Time.deltaTime);
        }

        public void SetParameter(string commentWord)
        {
            commentText.text = commentWord;
        }

        public void Daipaned()
        {
            IsActive = false;
            DaipanedSequence();
        }

        void DaipanedSequence()
        {
            var prePosition = commentText.transform.position;
            var sequence = DOTween.Sequence()
                .Append(commentText.transform.DOScaleY(0.3f, 0.2f).SetEase(Ease.InQuint)) // 縮めて、
                .Join(commentText.transform.DOMoveY(prePosition.y - 0.6f, 0.2f).SetEase(Ease.InQuint)) // 同時に下に移動
                .Append(commentText.transform.DOScaleY(1.1f, 0.15f).SetEase(Ease.InCubic)) // 素早く大きくする
                .Join(commentText.transform.DOMoveY(prePosition.y + 0.6f, 0.15f).SetEase(Ease.InCubic)) // 同時に上に移動
                .Append(commentText.transform.DOScaleY(0, 0.4f).SetEase(Ease.InCubic)) // 小さくしながら
                .Join(commentText.transform.DOMoveY(prePosition.y - 1, 0.4f).SetEase(Ease.InCubic)) // 同時に下に移動
                .OnComplete(() =>
                {
                    _antiCommentCluster.Remove(this);
                    Destroy(gameObject);
                });
            sequence.Play();
        }


    }
}

