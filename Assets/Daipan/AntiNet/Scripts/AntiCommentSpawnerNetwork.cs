#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts.Utility;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiCommentSpawnerNetwork
    {
        readonly IPrefabLoader<AntiCommentNet> _antiCommentLoader;
        readonly IPrefabLoader<SpecialAntiCommentNet> _specialAntiCommentLoader;
        readonly CommentParamsServer _commentParamsServer;
        readonly AntiCommentCluster _antiCommentCluster;
        readonly AntiCommentObserver _antiCommentObserver;
        readonly AntiStateValue _antiStateValue;
        readonly IAntiCommentParam _antiCommentParam;
        readonly NetworkRunner _runner;
        readonly IViewerNumber _viewerNumber;
        private AntiCommentNet? _antiCommentPrefab;


        [Inject]
        public AntiCommentSpawnerNetwork(
            IPrefabLoader<AntiCommentNet> antiCommentLoader,
            IPrefabLoader<SpecialAntiCommentNet> specialAntiCommentLoader,
            CommentParamsServer commentParamsServer,
            AntiCommentCluster antiCommentCluster,
            AntiCommentObserver antiCommentObserver,
            AntiStateValue antiStateValue,
            IAntiCommentParam antiCommentParam,
            NetworkRunner runner,
            IViewerNumber viewerNumber
        )
        {
            _antiCommentLoader = antiCommentLoader;
            _specialAntiCommentLoader = specialAntiCommentLoader;
            _commentParamsServer = commentParamsServer;
            _antiCommentCluster = antiCommentCluster;
            _antiCommentObserver = antiCommentObserver;
            _antiStateValue = antiStateValue;
            _antiCommentParam = antiCommentParam;
            _runner = runner;
            _viewerNumber = viewerNumber;
        }

        public void SpawnAntiComment(string commentWord)
        {
            if (commentWord == "")
            {
                Debug.Log("CommentWord is null");
                return;
            }
            if (IsTextMoreThan(commentWord))
            {
                Debug.Log($"{commentWord} is longer than {_antiCommentParam.MaxCharacterNumber}");
                return; // 警告だすかも？
            }


            if (_antiStateValue.IsSpecialFever)
            {
                SpawnSpecialAntiComment(commentWord);
            }
            else
            {
                SpawnNormalAntiComment(commentWord);
            }


            if (_antiStateValue.AntiStateEnum == AntiStateEnum.FEVER)
            {
                _antiCommentObserver.ResetCount();
            }
            else
            {
                _antiCommentObserver.UpCount();
            }
        }
        void SpawnNormalAntiComment(string commentWord)
        {
            if (_antiCommentPrefab == null)
                _antiCommentPrefab = _antiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();

            var antiComment = _runner.Spawn(_antiCommentPrefab, spawnPosition, Quaternion.identity);

            antiComment.SetParameter(commentWord);
            _antiCommentCluster.Add(antiComment);

            var multipliedAmount = _commentParamsServer.GetViewerDiffAntiCommentNumber();
            _viewerNumber.DecreaseViewer(multipliedAmount);
        }

        void SpawnSpecialAntiComment(string commentWord)
        {
            var specialAntiCommentPrefab = _specialAntiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();

            var antiComment = _runner.Spawn(specialAntiCommentPrefab, spawnPosition, Quaternion.identity);

            antiComment.SetParameter(commentWord);
            _antiCommentCluster.Add(antiComment);

            var multipliedAmount = _commentParamsServer.GetViewerDiffAntiCommentNumber();
            _viewerNumber.DecreaseViewer(multipliedAmount);
        }


        bool IsTextMoreThan(string text)
        {
            return text.Length > _antiCommentParam.MaxCharacterNumber;
        }
    }
}