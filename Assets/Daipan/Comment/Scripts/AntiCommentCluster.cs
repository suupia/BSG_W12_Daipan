using System.Collections.Generic;
using Daipan.Comment.MonoScripts;
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    
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
    }
    public void Daipaned(float probability = 1.0f)
    {
        var comments = _comments.ToArray();
        foreach (var comment in comments)
            if (Random.value < probability)
                comment.Daipaned();
    }

}
}
