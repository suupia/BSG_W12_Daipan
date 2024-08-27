#nullable enable
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Daipan.LevelDesign.Comment.Scripts
{
    public class CommentPosition : MonoBehaviour
    {
        [Header("コメントの座標たちをセットする場所です！")]
        [Space(30)]


        [Header("コメントの生成位置")]
        [Tooltip("コメントの生成位置を示すGameObjectを入れて！！")]
        public Transform CommentSpawnedPoint = null!;
        [Header("上下にnの幅でコメントを生成")]
        [Min(0)]
        public float SpawnBand = 0;


        [Header("コメントの消滅位置")]
        [Tooltip("コメントの消滅位置を示すGameObjectを入れて！！")]
        public Transform CommentDespawnedPoint = null!;

        [Header("アンチコメントの生成エリア")]
        public BoxCollider2D AntiCommentSpawnArea = null!;


        [Header("コメントの親オブジェクト")]
        [Tooltip("コメントの親オブジェクトを入れて！！")]
        public Transform CommentParent = null!;

        [Header("アンチコメントの親オブジェクト")]
        [Tooltip("アンチコメントの親オブジェクトを入れて！！")]
        public Transform AntiCommentParent = null!;
    }
}