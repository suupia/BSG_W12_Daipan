#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Fusion;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using VContainer;
using Daipan.Stream.Scripts;
using Daipan.Comment.Interfaces;
using Daipan.Daipan;
using R3;
using System;
using Daipan.Stream.Interfaces;
using DG.Tweening;
using Daipan.Player.Scripts;

namespace Daipan.Comment.MonoScripts
{
    public class SpecialAntiCommentNet : NetworkBehaviour, IAntiCommentMono
    {
        [SerializeField] TextMeshPro commentText = null!;

        AntiCommentCluster _antiCommentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;
        IIrritatedGaugeValue _irritatedGaugeValue = null!;
        NetworkRunner _runner = null!;
        bool IsActive { get; set; } = true;
        int _hp;
        [Networked]
        [OnChangedRender(nameof(OnCommentTextChanged))]
        public NetworkString<_8> CommentText { get; set; }

        public override void Spawned()
        {
            base.Spawned();
            var daipanScopeNet = DaipanScopeNet.BuiltContainer;
            Initialize(
                daipanScopeNet.Container.Resolve<AntiCommentCluster>()
               , daipanScopeNet.Container.Resolve<CommentParamsServer>()
               , daipanScopeNet.Container.Resolve<IIrritatedGaugeValue>()
               , daipanScopeNet.Container.Resolve<IAntiCommentParam>()
               , daipanScopeNet.Container.Resolve<NetworkRunner>()
            );

            OnCommentTextChanged();
        }

        public void Initialize(
            AntiCommentCluster antiCommentCluster
            , CommentParamsServer commentParamsServer
            , IIrritatedGaugeValue irritatedGaugeValue
            , IAntiCommentParam antiCommentParam
            , NetworkRunner networkRunner
        )
        {
            _antiCommentCluster = antiCommentCluster;
            _commentParamsServer = commentParamsServer;
            _irritatedGaugeValue = irritatedGaugeValue;
            _runner = networkRunner;

            _hp = antiCommentParam.SpecialAntiCommentHp;
        }

        void Update()
        {
            // if (IsActive) _irritatedGaugeValue.IncreaseValue(_commentParamsServer.GetIrritationIncreasePerSec() * Time.deltaTime);
        }

        public void SetParameter(string commentWord)
        {
            CommentText = commentWord;
        }

        public void Daipaned()
        {
            _hp--;
            if (_hp > 0)
            {
                return;
            }
            IsActive = false;

            gameObject.layer = LayerMask.NameToLayer("AntiComment");
            foreach (Transform child in transform)
            {
                child.gameObject.layer = LayerMask.NameToLayer("AntiComment");
            }

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
                    _runner.Despawn(gameObject.GetComponent<NetworkObject>());
                });
            sequence.Play();
        }

        void OnCommentTextChanged()
        {
            commentText.text = (string)CommentText;
        }
    }
}