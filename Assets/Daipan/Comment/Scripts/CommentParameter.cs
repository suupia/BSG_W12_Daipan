#nullable enable
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/Parameter", order = 1)]
    public sealed class CommentParameter : ScriptableObject
    {
        [SerializeField] CommentType commentType = CommentType.None;
        [SerializeField] Sprite sprite = null!;
        public CommentType CommentType => commentType;
        public Sprite Sprite => sprite;
    }

    public enum CommentType
    {
        None,
        Normal,
        Super,
        Spiky,
    }
}