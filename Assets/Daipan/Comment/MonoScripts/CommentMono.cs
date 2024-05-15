#nullable enable
using Daipan.Comment.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Comment.MonoScripts
{
    public class CommentMono : MonoBehaviour
    {
        [SerializeField] float speed = 0.01f;

        CommentSpawnPointContainer _spawnPointContainer = null!;
        CommentCluster _commentCluster = null!;

        void Update()
        {
            transform.position += Vector3.up * speed;
            if (transform.position.y > _spawnPointContainer.DespawnPosition.y) Destroy(gameObject);
        }

        [Inject]
        public void Initialize(
            CommentSpawnPointContainer spawnPointContainer,
            CommentCluster commentCluster
            )
        {
            _spawnPointContainer = spawnPointContainer;
            _commentCluster = commentCluster;
        }

        public void BlownAway()
        {
            _commentCluster.Remove(this);
        }
        
        
    }
}