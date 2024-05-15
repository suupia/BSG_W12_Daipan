#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Comment.MonoScripts;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

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
            comment.Despawn();
        }

        public void BlownAway(float probability = 1.0f)
        {
            var comments = _comments.ToArray();
            foreach (var comment in comments)
                if (Random.value < probability)
                    comment.BlownAway();
        }

        public void BlownAway(Func<CommentMono, bool> blowAwayCondition)
        {
            var comments = _comments.ToArray();
            foreach (var comment in comments)
                if (blowAwayCondition(comment))
                    comment.BlownAway();
        }
    }
}