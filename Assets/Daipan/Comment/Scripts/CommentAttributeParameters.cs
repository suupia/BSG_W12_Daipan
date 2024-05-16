#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/CommentParameter", order = 1)]
    public sealed class CommentAttributeParameters
    {
        public List<CommentParameter> commentParameters = new ();
    }
}