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
        CommentAttributeParameters _attributeParameters = null!;
        CommentCluster _commentCluster = null!;
        CommentSpawnPointContainer _commentSpawnPointContainer = null!;

        IObjectResolver _container = null!;
        IPrefabLoader<CommentMono> _loader = null!;

        // todo : 後で分離する
        ViewerNumber _viewerNumber = null!;

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) SpawnComment(CommentEnum.Normal);
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

        public void SpawnComment(CommentEnum commentEnum)
        {
            var commentPrefab = _loader.Load();
            var comment = _container.Instantiate(commentPrefab, _commentSpawnPointContainer.SpawnPosition,
                Quaternion.identity, commentSection.transform);
            var parameter = _attributeParameters.CommentParameters.First(c => c.GetCommentEnum == commentEnum);
            comment.SetParameter(parameter);
            comment.OnDespawn += (sender, args) =>
            {
                if (args.CommentEnum == CommentEnum.Super) _viewerNumber.IncreaseViewer(30);
                if (args.CommentEnum == CommentEnum.Spiky) _viewerNumber.DecreaseViewer(10);
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