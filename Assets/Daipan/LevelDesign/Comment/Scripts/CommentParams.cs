using Daipan.Comment.Scripts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Daipan.Utility.Scripts;
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
        [SerializeField] public Sprite sprite = null!;


        [Header("コメントが流れる速度(unit/s)")]
        [Min(0)]
        public float commentSpeed_ups;

        [Header("コメントが上部へ到達したときの視聴者数の変化量")]
        public int diffViewer;


        public CommentEnum GetCommentEnum
        {
            get
            {
                EnumEnumerationChecker.CheckEnum<CommentType,CommentEnum>();
                return CommentEnum.Values.First(x => x.Name == commentType.ToString());
            }
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