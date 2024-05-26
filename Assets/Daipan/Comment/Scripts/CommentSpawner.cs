using Daipan.Comment.MonoScripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.Scripts;

public sealed class CommentSpawner : IUpdate
{
    readonly CommentCluster _commentCluster;
    readonly IPrefabLoader<CommentMono> _commentLoader;
    readonly CommentParamsServer _commentParamsServer;

    readonly IObjectResolver _container;

    // todo : 後で分離する
    readonly ViewerNumber _viewerNumber;
    IPrefabLoader<AntiCommentMono> _antiCommentLoader;
    
    public CommentSpawner(
        IObjectResolver container,
        CommentParamsServer commentParamsServer,
        CommentCluster commentCluster,
        ViewerNumber viewerNumber,
        IPrefabLoader<CommentMono> commentCommentLoader,
        IPrefabLoader<AntiCommentMono> antiCommentLoader
    )
    {
        _container = container;
        _commentParamsServer = commentParamsServer;
        _commentCluster = commentCluster;
        _viewerNumber = viewerNumber;
        _commentLoader = commentCommentLoader;
        _antiCommentLoader = antiCommentLoader;
    }

    void IUpdate.Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SpawnComment(CommentEnum.Normal);
    }


    public void SpawnComment(CommentEnum commentEnum)
    {
        Debug.Log($"commentEnum: {commentEnum}");
        var commentPrefab = _commentLoader.Load();
        var comment = _container.Instantiate(commentPrefab, _commentParamsServer.GetSpawnedPosition(),
            Quaternion.identity, _commentParamsServer.GetCommentParent());
        comment.SetParameter(commentEnum);
        comment.OnDespawn += (sender, args) =>
        {
            var amount = _commentParamsServer.GetViewerDiffNumber(args.CommentEnum);
            if (amount > 0) _viewerNumber.IncreaseViewer(amount);
            else _viewerNumber.DecreaseViewer(-amount);
        };
        _commentCluster.Add(comment);
        Debug.Log($"Comment spawned: {commentEnum}");
    }
}