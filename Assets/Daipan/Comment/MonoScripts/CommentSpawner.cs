#nullable enable
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.MonoScripts
{
    public sealed class CommentSpawner : MonoBehaviour
    {
        [SerializeField] GameObject commentSection = null!;
        [SerializeField] CommentMono superCommentPrefab = null!;
        CommentAttributeParameters _attributeParameters = null!;
        CommentCluster _commentCluster = null!;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;

        IObjectResolver _container = null!;
        IPrefabLoader<CommentMono> _loader = null!;

        // todo : 後で分離する
        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var commentPrefab = _loader.Load();
                var comment = _container.Instantiate(commentPrefab, _commentSpawnPointContainer.SpawnPosition,
                    Quaternion.identity,
                    commentSection.transform);
                _commentCluster.Add(comment);
                comment.SetParameter(
                    _attributeParameters.CommentParameters.First(c => c.CommentType == CommentType.Normal));
            }
        }

        [Inject]
        public void Initialize(
            IPrefabLoader<CommentMono> loader,
            CommentAttributeParameters attributeParameters
        )
        {
            _loader = loader;
            _attributeParameters = attributeParameters;
        }

        public void SpawnSuperComment()
        {
            var commentPrefab = _loader.Load();
            var comment = _container.Instantiate(superCommentPrefab, _commentSpawnPointContainer.SpawnPosition,
                Quaternion.identity,
                commentSection.transform);
            comment.SetParameter(_attributeParameters.CommentParameters.First(c => c.CommentType == CommentType.Super));
            comment.OnDespawn += (sender, args) => { _viewerNumber.IncreaseViewer(30); };
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