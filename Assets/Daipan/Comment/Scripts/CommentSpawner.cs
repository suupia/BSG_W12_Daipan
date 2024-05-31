using Daipan.Comment.MonoScripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Daipan.Comment.Scripts
{
   public sealed class CommentSpawner : IUpdate
{
    readonly AntiCommentCluster _antiCommentCluster;
    readonly IPrefabLoader<AntiCommentMono> _antiCommentLoader;
    readonly CommentCluster _commentCluster;
    readonly IPrefabLoader<CommentMono> _commentLoader;
    readonly CommentParamsServer _commentParamsServer;

    readonly IObjectResolver _container;

    // todo : 後で分離する
    readonly ViewerNumber _viewerNumber;

    public CommentSpawner(
        IObjectResolver container,
        CommentParamsServer commentParamsServer,
        CommentCluster commentCluster,
        AntiCommentCluster antiCommentCluster,
        ViewerNumber viewerNumber,
        IPrefabLoader<CommentMono> commentCommentLoader,
        IPrefabLoader<AntiCommentMono> antiCommentLoader
    )
    {
        _container = container;
        _commentParamsServer = commentParamsServer;
        _commentCluster = commentCluster;
        _antiCommentCluster = antiCommentCluster;
        _viewerNumber = viewerNumber;
        _commentLoader = commentCommentLoader;
        _antiCommentLoader = antiCommentLoader;
    }

    void IUpdate.Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) SpawnComment(CommentEnum.Normal);
    }


    public void SpawnCommentByType(CommentEnum commentEnum)
    {
        if (commentEnum == CommentEnum.Normal) SpawnComment(commentEnum);
        else if (commentEnum == CommentEnum.Super) SpawnComment(commentEnum);
        else if (commentEnum == CommentEnum.Spiky) SpawnAntiComment();
    }

    void SpawnComment(CommentEnum commentEnum)
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

    void SpawnAntiComment()
    {
        var commentPrefab = _antiCommentLoader.Load();
        var spawnPosition = Vector3.zero; // todo : とりあえず画面中央に配置 配置が面倒なら、親オブジェクト指定してVerticalLayoutGroupを使う
        var comment = _container.Instantiate(commentPrefab, spawnPosition,
            Quaternion.identity, _commentParamsServer.GetCommentParent());
        comment.SetParameter(CommentEnum.Super);
        comment.OnDespawn += (sender, args) =>
        {
            var amount = _commentParamsServer.GetViewerDiffNumber(args.CommentEnum);
            if (amount > 0) _viewerNumber.IncreaseViewer(amount);
            else _viewerNumber.DecreaseViewer(-amount);
        };
        _antiCommentCluster.Add(comment);
        Debug.Log($"Comment spawned: {CommentEnum.Super}");
    }
} 
}

