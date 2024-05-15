#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.MonoScripts
{
    public class CommentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject commentSection = null!; 
        [SerializeField] CommentMono commentPrefab = null!;
        [SerializeField] CommentMono superCommentPrefab = null!;

        IObjectResolver _container = null!;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;
        CommentCluster _commentCluster = null!;
        
        // todo : 後で分離する
        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var comment = _container.Instantiate(commentPrefab, _commentSpawnPointContainer.SpawnPosition, Quaternion.identity,
                    commentSection.transform);
                _commentCluster.Add(comment);
                comment.IsSuperComment = false;
            }
        }
        
        public void SpawnSuperComment()
        {
            var comment = _container.Instantiate(superCommentPrefab, _commentSpawnPointContainer.SpawnPosition, Quaternion.identity,
                commentSection.transform);
            comment.IsSuperComment = true;
            comment.OnDespawn += (sender, args) =>
            {
                _viewerNumber.IncreaseViewer(30);
            };
            _commentCluster.Add(comment);
        }

        [Inject]
        public void Initialized(
            IObjectResolver container,
            CommentSpawnPointContainer commentSpawnPointContainer,
            CommentCluster commentCluster,
            ViewerNumber viewerNumber
            )
        
        {
            _container = container;
            _commentSpawnPointContainer = commentSpawnPointContainer;
            _commentCluster = commentCluster;
            _viewerNumber = viewerNumber;
        }
    }
}