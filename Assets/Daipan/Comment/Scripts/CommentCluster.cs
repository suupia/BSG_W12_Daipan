#nullable enable
using UnityEngine;
using System.Collections.Generic;
using Daipan.Comment.MonoScripts;

namespace Daipan.Comment.Scripts
{
    public class CommentCluster
    {
        readonly List<CommentMono> _comments = new();
        public IEnumerable<CommentMono> CommentMonos => _comments;

        public void Add(CommentMono comment)
        {
            _comments.Add(comment);
        }
        public void Remove(CommentMono comment)
        {
            _comments.Remove(comment);
            Object.Destroy(comment.gameObject);
        }

        public void BlownAway(float probability = 1.0f)
        {
            var comments = _comments.ToArray();
            foreach (var comment in comments)
            {
                if (Random.value < probability) comment.BlownAway();
            }
        }
    }
}