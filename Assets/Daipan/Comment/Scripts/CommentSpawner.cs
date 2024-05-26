#nullable enable
using System.Linq;
using Daipan.Comment.MonoScripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using Unity.Mathematics;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.Scripts
{
    public sealed class CommentSpawner : IUpdate
    {
        CommentParamsServer _commentParamsServer = null!;
        CommentCluster _commentCluster = null!;

        IObjectResolver _container = null!;
        IPrefabLoader<CommentMono> _loader = null!;

        // todo : 後で分離する
        ViewerNumber _viewerNumber = null!;

        void IUpdate.Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) SpawnComment(CommentEnum.Normal);
        }

        [Inject]
        public void Initialize(
            IPrefabLoader<CommentMono> loader
        )
        {
            _loader = loader;
        }

        public void SpawnComment(CommentEnum commentEnum)
        {
            Debug.Log($"commentEnum: {commentEnum}");
            var commentPrefab = _loader.Load();
            var comment = _container.Instantiate(commentPrefab, _commentParamsServer.GetSpawnedPosition(),
                Quaternion.identity, _commentParamsServer.GetCommentParent());
            comment.SetParameter(commentEnum);
            comment.OnDespawn += (sender, args) =>
            {
                int amount = _commentParamsServer.GetViewerDiffNumber(args.CommentEnum);
                if (amount > 0) _viewerNumber.IncreaseViewer(amount);
                else _viewerNumber.DecreaseViewer(-amount);
            };
            _commentCluster.Add(comment);
            Debug.Log($"Comment spawned: {commentEnum}");
        }
        

        [Inject]
        public void Initialized(
            IObjectResolver container,
            CommentParamsServer commentParamsServer,
            CommentCluster commentCluster,
            ViewerNumber viewerNumber
        )

        {
            _container = container;
            _commentParamsServer = commentParamsServer;
            _commentCluster = commentCluster;
            _viewerNumber = viewerNumber;
        }
    }
}