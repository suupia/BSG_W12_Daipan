#nullable enable
using System;
using Daipan.Comment.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Comment.MonoScripts
{
    public class CommentMono : MonoBehaviour
    {
        [SerializeField] float speed = 0.01f;
        CommentCluster _commentCluster = null!;
        CommentSpawnPointContainer _spawnPointContainer = null!;
        public bool IsSuperComment { get; set; }

        void Update()
        {
            transform.position += Vector3.up * speed;
            if (transform.position.y > _spawnPointContainer.DespawnPosition.y) _commentCluster.Remove(this);
        }

        public event EventHandler<DespawnEventArgs>? OnDespawn;


        [Inject]
        public void Initialize(
            CommentSpawnPointContainer spawnPointContainer,
            CommentCluster commentCluster
        )
        {
            _spawnPointContainer = spawnPointContainer;
            _commentCluster = commentCluster;
        }

        public void Despawn()
        {
            var args = new DespawnEventArgs(IsSuperComment);
            OnDespawn?.Invoke(this, args);
            Destroy(gameObject);
        }

        public void BlownAway()
        {
            _commentCluster.Remove(this);
        }


        public void SetParameter(bool isSuperComment)
        {
            IsSuperComment = isSuperComment;
            // GetComponent<SpriteRenderer>().color = isSuperComment ? Color.red : Color.white;
        }
    }

    public record DespawnEventArgs(bool IsSuperComment);
}