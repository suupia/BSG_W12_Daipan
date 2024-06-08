#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using TMPro;
using UnityEngine;
using VContainer;

namespace Daipan.Comment.MonoScripts
{
    public sealed class CommentMono : MonoBehaviour
    {
        [SerializeField] SpriteRenderer spriteRenderer = null!;
        [SerializeField] TextMeshPro commentText = null!;
        CommentCluster _commentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;

        string _commentWord = null!;

        void Update()
        {
            var direction = (new Vector3(_commentParamsServer.GetDespawnedPosition().x - transform.position.x, 0, 0)).normalized;
            transform.position += direction * _commentParamsServer.GetSpeed() * Time.deltaTime;
            if (transform.position.x - _commentParamsServer.GetDespawnedPosition().x < 0.001f) _commentCluster.Remove(this);
        }

        public event EventHandler<DespawnEventArgs>? OnDespawn;


        [Inject]
        public void Initialize(
            CommentParamsServer commentParamsServer,   
            CommentCluster commentCluster
        )
        {
            _commentParamsServer = commentParamsServer;
            _commentCluster = commentCluster;
        }

        public void SetParameter(string commentWord)
        {
            _commentWord = commentWord;
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
            _commentCluster.Remove(this);
        }
    }

        public record DespawnEventArgs(CommentEnum CommentEnum);

}