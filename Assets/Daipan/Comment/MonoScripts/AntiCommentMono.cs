#nullable enable
using System;
using Daipan.Comment.Scripts;
using UnityEngine;
using VContainer;
using TMPro;

namespace Daipan.Comment.MonoScripts
{
    public sealed class AntiCommentMono : MonoBehaviour
    {
        [SerializeField] TextMeshPro commentText = null!;

        AntiCommentCluster _antiCommentCluster = null!;

        string _antiCommentWord = null!;

        public event EventHandler<DespawnEventArgs>? OnDespawn;
        
        
        [Inject]
        public void Initialize(
            AntiCommentCluster antiCommentCluster
        )
        {
            _antiCommentCluster = antiCommentCluster;
        }


        public void SetParameter(string commentWord)
        {
            _antiCommentWord = commentWord;
            commentText.text = commentWord;
        }

        public void Despawn()
        {
            var args = new DespawnEventArgs(CommentEnum.None);
            OnDespawn?.Invoke(this, args);
            Destroy(gameObject);
        }
        public void BlownAway()
        {
            _antiCommentCluster.Remove(this);
        }


    } 
}

