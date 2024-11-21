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

namespace Daipan.Comment.MonoScripts
{
    public class AntiCommentNet : NetworkBehaviour, IAntiCommentMono
    {
        [SerializeField] TextMeshPro commentText = null!;

        AntiCommentCluster _antiCommentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;
        IrritatedGaugeValue _irritatedGaugeValue = null!;
        bool IsActive { get; set; } = true;
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
               , daipanScopeNet.Container.Resolve<IrritatedGaugeValue>()
            );

            OnCommentTextChanged();
        }

        public void Initialize(
            AntiCommentCluster antiCommentCluster
            , CommentParamsServer commentParamsServer
            , IrritatedGaugeValue irritatedGaugeValue
        )
        {
            _antiCommentCluster = antiCommentCluster;
            _commentParamsServer = commentParamsServer;
            _irritatedGaugeValue = irritatedGaugeValue;
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
            IsActive = false;
            // DaipanedSequence();
        }

        void OnCommentTextChanged()
        {
            commentText.text = (string)CommentText;
        }
    }
}