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
        readonly CommentParamsManager _commentParamsManager;
        readonly CommentPosition _commentPosition;
        

        [Inject]
        CommentParamsServer (CommentParamsManager commentParamsManager, CommentPosition commentPosition)
        {
            _commentParamsManager = commentParamsManager;
            _commentPosition = commentPosition;
        }


        // CommentManagerParamsについて

        #region Params

        public Sprite GetSprite(CommentEnum commentEnum)
        {
            var cparams = GetCommentParams(commentEnum);
            return cparams.sprite;
        }

        public float GetSpeed(CommentEnum commentEnum)
        {
            if (_commentParamsManager.isAdaptSameSpeed) return _commentParamsManager.commentSpeed;
            var cparams = GetCommentParams(commentEnum);
            return cparams.commentSpeed_ups;
        }

        public int GetViewerDiffNumber(CommentEnum commentEnum)
        {
            return GetCommentParams(commentEnum).diffViewer;
        }
        
        
        CommentParams GetCommentParams(CommentEnum commentEnum)
        {
            return _commentParamsManager.commentParams.First(c => c.GetCommentEnum == commentEnum);
        }

        #endregion

        // CommentPositionについて
        #region Position 
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