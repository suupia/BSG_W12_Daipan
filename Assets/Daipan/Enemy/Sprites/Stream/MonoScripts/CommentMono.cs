#nullable enable
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;

namespace Daipan.Stream.MonoScripts
{
    public class CommentMono : MonoBehaviour
    {
        [SerializeField] float speed = 0.01f;

        CommentSpawnPointContainer _spawnPointContainer = null!;

        void Update()
        {
            transform.position += Vector3.up * speed;
            if (transform.position.y > _spawnPointContainer.DespawnPosition.y) Destroy(gameObject);
        }

        [Inject]
        public void Initialize(CommentSpawnPointContainer spawnPointContainer)
        {
            _spawnPointContainer = spawnPointContainer;
        }
    }
}