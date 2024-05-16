#nullable enable
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/Parameter", order = 1)]
    public sealed class CommentParameter : ScriptableObject
    {
        [SerializeField] CommentType commentType = CommentType.None;
    }

    public enum CommentType
    {
        None,
        Normal,
        Super,
        Spiky,
    }
}