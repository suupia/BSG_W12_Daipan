#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Daipan.LevelDesign.Comment.Scripts
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Comment/ManagerParameters", order = 1)]
    public class CommentParamsManager : ScriptableObject
    {
        [Header("コメント全体のレベルデザインはこちら！！")]
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
    }
}