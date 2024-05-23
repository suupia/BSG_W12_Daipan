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

        public CommentEnum _commentEnum { get; private set; } = CommentEnum.None;
        
        void Update()
        {
            transform.position += Vector3.up * _commentParamsServer.GetSpeed(_commentEnum) * Time.deltaTime;
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

        public void SetParameter(CommentEnum commentEnum)
        {
            _commentEnum = commentEnum;
            spriteRenderer.sprite = _commentParamsServer.GetSprite(commentEnum);
            Debug.Log($"spriteRenderer.sprite : {spriteRenderer.sprite}");
        }

        public void Despawn()
        {
            var args = new DespawnEventArgs(_commentEnum);
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