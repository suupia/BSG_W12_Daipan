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
        readonly IPrefabLoader<SpecialAntiCommentNet> _specialAntiCommentLoader;
        readonly CommentParamsServer _commentParamsServer;
        readonly AntiCommentCluster _antiCommentCluster;
        readonly AntiCommentObserver _antiCommentObserver;
        readonly AntiStateValue _antiStateValue;
        readonly NetworkRunner _runner;


        [Inject]
        public AntiCommentSpawnerNetwork(
            IPrefabLoader<AntiCommentNet> antiCommentLoader,
            IPrefabLoader<SpecialAntiCommentNet> specialAntiCommentLoader,
            CommentParamsServer commentParamsServer,
            AntiCommentCluster antiCommentCluster,
            AntiCommentObserver antiCommentObserver,
            AntiStateValue antiStateValue,
            NetworkRunner runner
        )
        {
            _antiCommentLoader = antiCommentLoader;
            _specialAntiCommentLoader = specialAntiCommentLoader;
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
            var antiCommentPrefab = _antiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();

            var antiComment = _runner.Spawn(antiCommentPrefab, spawnPosition, Quaternion.identity);

            antiComment.SetParameter(commentWord);
            _antiCommentCluster.Add(antiComment);
        }

        void SpawnSpecialAntiComment(string commentWord)
        {
            var specialAntiCommentPrefab = _specialAntiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();

            var antiComment = _runner.Spawn(specialAntiCommentPrefab, spawnPosition, Quaternion.identity);

            antiComment.SetParameter(commentWord);
            _antiCommentCluster.Add(antiComment);
        }


        bool IsTextMoreThan(string text)
        {
            return text.Length > maxCharacterNum;
        }
    }
}