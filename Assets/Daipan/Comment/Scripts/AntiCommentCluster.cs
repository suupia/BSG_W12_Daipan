using System.Collections.Generic;
using Daipan.Comment.Interfaces;
using Daipan.Comment.MonoScripts;
using UnityEngine;

namespace Daipan.Comment.Scripts
{

    public sealed class AntiCommentCluster
    {
        readonly List<IAntiCommentMono> _comments = new();
        public IEnumerable<IAntiCommentMono> CommentMonos => _comments;

        public void Add(IAntiCommentMono comment)
        {
            _comments.Add(comment);
        }

        public void Remove(IAntiCommentMono comment)
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
