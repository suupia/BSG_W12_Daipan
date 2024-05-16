#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/AttributeParameters", order = 1)]
    public sealed class CommentAttributeParameters : ScriptableObject
    {
        [SerializeField] List<CommentParameter> commentParameters = new ();
        public IReadOnlyList<CommentParameter> CommentParameters => commentParameters;
    }
}