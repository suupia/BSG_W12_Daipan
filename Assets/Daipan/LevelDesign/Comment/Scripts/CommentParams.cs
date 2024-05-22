using Daipan.Comment.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Daipan.LevelDesign.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/Parameters", order = 1)]
    public sealed class CommentParams : ScriptableObject
    {
        [Header("コメントのレベルデザインはこちら！！")]
        [Space(30)]


        [Header("コメントのタイプ")]
        [Tooltip("None      : 設定しないでください-_-\n" +
                 "Normal    : ")]
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