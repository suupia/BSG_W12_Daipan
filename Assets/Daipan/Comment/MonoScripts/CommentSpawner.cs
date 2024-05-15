#nullable enable
using Daipan.Comment.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.MonoScripts
{
    public class CommentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject commentSection = null!; // [Prerequisite
        [SerializeField] CommentMono commentPrefab = null!;

        IObjectResolver _container = null!;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;
        CommentCluster _commentCluster = null!;


        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var comment = _container.Instantiate(commentPrefab, _commentSpawnPointContainer.SpawnPosition, Quaternion.identity,
                    commentSection.transform);
                _commentCluster.Add(comment);
            }
        }

        [Inject]
        public void Initialized(
            IObjectResolver container,
            CommentSpawnPointContainer commentSpawnPointContainer,
            CommentCluster commentCluster)
        
        {
            _container = container;
            _commentSpawnPointContainer = commentSpawnPointContainer;
            _commentCluster = commentCluster;
        }
    }
}