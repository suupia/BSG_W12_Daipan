#nullable enable
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/CommentParamManager", order = 1)]
    public class CommentParamManager : ScriptableObject
    {
        [Header("コメント全体のレベルデザインはこちら。")]
        [Space(30)]


        [Header("コメントの流れる速度")]
        [Min(0)] public float commentSpeed;

        [Header("コメントが流れ切ったときのViewer(高評価)の変化量")]
        public int diffCommentViewer;

        [Header("アンチコメントが時間あたりのViewer(高評価)の変化量")]
        public int diffAntiCommentViewer;

        [Header("コメント集")]
        public List<string> CommentWords = new ();

        [Header("アンチコメント集")]
        public List<string> AntiCommentWords = new ();
        
        public CommentParamDependOnViewer commentParamDependOnViewer = null!;
        public AntiCommentParamDependOnViewer antiCommentParamDependOnViewer = null!;
        
    }

    [Serializable]
    public sealed class CommentParamDependOnViewer
    {
        [Header("視聴者がこの数より多い時に、")]
        [Min(0)] public int viewerAmount;
        [Header("これだけコメントを生成する")]
        [Min(0)] public int commentAmount;
    }
    
    [Serializable]
    public sealed class AntiCommentParamDependOnViewer
    {
        [Header("視聴者がこの数より多い時に、")]
        [Min(0)] public int viewerAmount;
        [Header("これだけアンチコメントを生成する")]
        [Min(0)] public int commentAmount;
    }
}