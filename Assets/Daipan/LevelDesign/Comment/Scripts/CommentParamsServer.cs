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
        readonly CommentParamManager _commentParamManager;
        readonly CommentPosition _commentPosition;

        [Inject]
        CommentParamsServer (CommentParamManager commentParamManager, CommentPosition commentPosition)
        {
            _commentParamManager = commentParamManager;
            _commentPosition = commentPosition;
        }


        // CommentManagerParamsについて

        public float GetSpeed()
        {
            return _commentParamManager.commentSpeed;
        }
        
        public int GetViewerDiffCommentNumber()
        {
            return _commentParamManager.diffCommentViewer;
        }
        
        public int GetViewerDiffAntiCommentNumber()
        {
            return _commentParamManager.diffAntiCommentViewer;
        }
        
        public double GetIrritationIncreasePerSec()
        {
            return _commentParamManager.irritationIncreasePerSec;
        }

        public string GetRandomCommentWord()
        {
            int index = (int)(Random.value * _commentParamManager.commentWords.Count);
            return _commentParamManager.commentWords[index];
        }
        public string GetRandomAntiCommentWord()
        {
            int index = (int)(Random.value * _commentParamManager.antiCommentWords.Count);
            return _commentParamManager.antiCommentWords[index];
        }



        // CommentPositionについて
        public Vector3 GetSpawnedPosition()
        {
            float band = (Random.value - 0.5f) * 2 * _commentPosition.SpawnBand;
            return _commentPosition.CommentSpawnedPoint.position + new Vector3(0, band, 0);
        }

        public Vector3 GetDespawnedPosition()
        {
            return _commentPosition.CommentDespawnedPoint.position;
        }

        public Transform GetCommentParent()
        {
            return _commentPosition.CommentParent;
        }

        public Vector3 GetAntiSpawnedPosition()
        {
            Vector3 band = new Vector3(
                (Random.value - 0.5f) * _commentPosition.AntiCommentSpawnArea.size.x,
                (Random.value - 0.5f) * _commentPosition.AntiCommentSpawnArea.size.y,
                0
                );

            return _commentPosition.AntiCommentSpawnArea.gameObject.transform.position + band;

        }
        
        
        public CommentParamDependOnViewer GetCommentParamDependOnViewer()
        {
            return _commentParamManager.commentParamDependOnViewer;
        }

        public AntiCommentParamDependOnViewer GetAntiCommentParamDependOnViewer()
        {
            return _commentParamManager.antiCommentParamDependOnViewer;
        }
    }
}