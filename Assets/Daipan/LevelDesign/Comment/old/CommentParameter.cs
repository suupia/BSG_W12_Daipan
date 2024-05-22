#nullable enable
using System;
using System.Linq;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/Parameter", order = 1)]
    public sealed class CommentParameter : ScriptableObject
    {
        [SerializeField] CommentType commentType = CommentType.None;
        [SerializeField] Sprite sprite = null!;
        public Sprite Sprite => sprite;

        public CommentEnum GetCommentEnum
        {
            get
            {
                CommentEnumChecker.CheckEnum();
                return CommentEnum.Values.First(x => x.Name == commentType.ToString());
            }
        }
    }

    static class CommentEnumChecker
    {
        static bool _isCheckedEnum;

        public static void CheckEnum()
        {
            if (_isCheckedEnum) return;
            foreach (var type in Enum.GetValues(typeof(CommentType)).Cast<CommentType>())
            {
                var comment = CommentEnum.Values.FirstOrDefault(x => x.Name == type.ToString());
                if (comment.Equals(default(CommentEnum)))
                    Debug.LogWarning($"CommentEnum with name {type.ToString()} not found.");
            }

            _isCheckedEnum = true;
        }
    }

    enum CommentType
    {
        None,
        Normal,
        Super,
        Spiky,
    }
}