#nullable enable
using System.Collections;
using System.Collections.Generic;
using Daipan.Comment.MonoScripts;
using Daipan.Comment.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Stream.Scripts.Utility;
using Fusion;
using UnityEngine;
using VContainer;

namespace Daipan.AntiNet.Scripts
{
    public class AntiCommentSpawnerNetwork
    {
        // todo 値切り出す
        int maxCharacterNum = 5;
        //

        readonly IPrefabLoader<AntiCommentNet> _antiCommentLoader;
        readonly CommentParamsServer _commentParamsServer;
        readonly AntiCommentCluster _antiCommentCluster;
        readonly AntiCommentObserver _antiCommentObserver;
        readonly AntiStateValue _antiStateValue;
        readonly NetworkRunner _runner;


        [Inject]
        public AntiCommentSpawnerNetwork(
            IPrefabLoader<AntiCommentNet> antiCommentLoader,
            CommentParamsServer commentParamsServer,
            AntiCommentCluster antiCommentCluster,
            AntiCommentObserver antiCommentObserver,
            AntiStateValue antiStateValue,
            NetworkRunner runner
        )
        {
            _antiCommentLoader = antiCommentLoader;
            _commentParamsServer = commentParamsServer;
            _antiCommentCluster = antiCommentCluster;
            _antiCommentObserver = antiCommentObserver;
            _antiStateValue = antiStateValue;
            _runner = runner;
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
                Debug.Log($"{commentWord} is longer than {maxCharacterNum}");
                return; // 警告だすかも？
            }

            var antiCommentPrefab = _antiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();

            var antiComment = _runner.Spawn(antiCommentPrefab, spawnPosition, Quaternion.identity);

            antiComment.SetParameter(commentWord);
            _antiCommentCluster.Add(antiComment);


            if (_antiStateValue.AntiStateEnum == AntiStateEnum.FEVER)
            {
                _antiCommentObserver.ResetCount();
            }
            else
            {
                _antiCommentObserver.UpCount();
            }
        }

        bool IsTextMoreThan(string text)
        {
            return text.Length > maxCharacterNum;
        }
    }
}