using System.Collections.Generic;
using Daipan.Comment.MonoScripts;

namespace Daipan.Comment.Scripts;

public sealed class AntiCommentCluster
{
    readonly List<AntiMono> _comments = new();
    public IEnumerable<AntiMono> CommentMonos => _comments;

    public void Add(AntiMono comment)
    {
        _comments.Add(comment);
    }

    public void Remove(AntiMono comment)
    {
        _comments.Remove(comment);
        comment.Despawn();
    }
}