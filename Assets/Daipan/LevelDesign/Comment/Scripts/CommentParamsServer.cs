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
        public float GetSpeed()
        {
            return _commentParamsManager.commentSpeed;
        }

        public int GetViewerDiffCommentNumber()
        {
            return _commentParamsManager.diffCommentViewer;
        }
        public int GetViewerDiffAntiCommentNumber()
        {
            return _commentParamsManager.diffAntiCommentViewer;
        }

        public string GetRandomCommentWord()
        {
            int index = (int)(Random.value * _commentParamsManager.CommentWords.Count);
            return _commentParamsManager.CommentWords[index];
        }
        public string GetRandomAntiCommentWord()
        {
            int index = (int)(Random.value * _commentParamsManager.AntiCommentWords.Count);
            return _commentParamsManager.AntiCommentWords[index];
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