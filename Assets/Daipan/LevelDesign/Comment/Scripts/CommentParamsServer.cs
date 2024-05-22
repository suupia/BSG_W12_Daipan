#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Comment.Scripts;
using System.Linq;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using UnityEngine.Scripting;
using UnityEngine.U2D;
using VContainer;


namespace Daipan.LevelDesign.Comment.Scripts
{
    public class CommentParamsServer
    {
        readonly CommentManagerParams _commentManagerParams = null;
        readonly CommentPosition _commentPosition = null;
        

        [Inject]
        CommentParamsServer (CommentManagerParams commentManagerParams, CommentPosition commentPosition)
        {
            _commentManagerParams = commentManagerParams;
            _commentPosition = commentPosition;
        }


        // CommentManagerParamsについて

        #region MyRegion

        public Sprite GetSprite(CommentEnum commentEnum)
        {
            var cparams = GetCommentParams(commentEnum);
            return cparams.Sprite;
        }

        public float GetSpeed(CommentEnum commentEnum)
        {
            if (_commentManagerParams.isAdaptSameSpeed) return _commentManagerParams.commentSpeed;
            var cparams = GetCommentParams(commentEnum);
            return cparams.commentSpeed_ups;
        }

        public int GetViewerDiffNumber(CommentEnum commentEnum)
        {
            return GetCommentParams(commentEnum).diffViewer;
        }
        
        
        CommentParams GetCommentParams(CommentEnum commentEnum)
        {
            return _commentManagerParams.commentParams.First(c => c.GetCommentEnum == commentEnum);
        }

        #endregion

        // CommentPositionについて
        #region 
        public Vector3 GetSpawnedPosition()
        {
            return _commentPosition.CommentSpawnedPoint.position;
        }

        public Vector3 GetDespawnedPosition()
        {
            return _commentPosition.CommentDespawnedPoint.position;
        }

        public Transform GetCommentParent()
        {
            return _commentPosition.CommentParent;
        }

        #endregion



    }
}