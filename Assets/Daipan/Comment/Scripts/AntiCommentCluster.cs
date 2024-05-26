using System.Collections.Generic;
using Daipan.Comment.MonoScripts;

namespace Daipan.Comment.Scripts;

public sealed class AntiCommentCluster
{
    readonly List<AntiCommentMono> _comments = new();
    public IEnumerable<AntiCommentMono> CommentMonos => _comments;

    public void Add(AntiCommentMono comment)
    {
        _comments.Add(comment);
    }

    public void Remove(AntiCommentMono comment)
    {
        _comments.Remove(comment);
        comment.Despawn();
    }
}