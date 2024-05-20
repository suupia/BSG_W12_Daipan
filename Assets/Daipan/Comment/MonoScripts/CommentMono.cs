#nullable enable
using System;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Comment.MonoScripts
{
    public sealed class CommentMono : MonoBehaviour
    {
        [SerializeField] float speed = 0.01f;
        [SerializeField] SpriteRenderer spriteRenderer = null!;
        CommentCluster _commentCluster = null!;
        CommentParamsServer _commentParamsServer = null!;
        public CommentParameter Parameter { get; private set; } = null!;
        
        void Update()
        {
            transform.position += Vector3.up * speed;
            if (transform.position.y > _commentParamsServer.GetDespawnedPosition().y) _commentCluster.Remove(this);
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

        public void SetParameter(CommentParameter parameter)
        {
            Parameter = parameter;
            spriteRenderer.sprite = Parameter.Sprite;
            Debug.Log($"spriteRenderer.sprite : {spriteRenderer.sprite}");
        }

        public void Despawn()
        {
            var args = new DespawnEventArgs(Parameter.GetCommentEnum);
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