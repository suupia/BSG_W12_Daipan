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
        [Header("個別のコメントのレベルデザインはこちら！！")]
        [Space(30)]


        [Header("コメントのタイプ")]
        [Tooltip("None      : 設定しないでください-_-\n" +
                 "Normal    : 普通のコメント\n" +
                 "Super     : めっちゃいいときのコメント\n" +
                 "Spiky     : イライラとげとげコメント")]
        [SerializeField] CommentType commentType = CommentType.None;

        [Header("コメントの背景スプライト")]
        [SerializeField] Sprite sprite = null!;
        public Sprite Sprite => sprite;


        [Header("コメントが流れる速度(unit/s)")]
        [Min(0)]
        public float commentSpeed_ups;

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