#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
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

        public void Despawn()
        {
            Destroy(gameObject);
        }
        public void BlownAway()
        {
            _antiCommentCluster.Remove(this);
        }


    } 
}

