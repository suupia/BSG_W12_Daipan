using Daipan.Comment.MonoScripts;
using Daipan.Core.Interfaces;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using Daipan.Stream.Scripts.Utility;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using R3;
using Daipan.Player.MonoScripts;

namespace Daipan.Comment.Scripts
{
    public sealed class CommentSpawner : IUpdate
    {
        readonly AntiCommentCluster _antiCommentCluster;
        readonly IPrefabLoader<AntiCommentMono> _antiCommentLoader;
        readonly CommentCluster _commentCluster;
        readonly IPrefabLoader<CommentMono> _commentLoader;
        readonly CommentParamsServer _commentParamsServer;

        readonly IObjectResolver _container;

        // todo : 後で分離する
        readonly ViewerNumber _viewerNumber;
        readonly ComboCounter _comboCounter;
        readonly IComboMultiplier _comboMultiplier;

        
        public CommentSpawner(
            IObjectResolver container,
            CommentParamsServer commentParamsServer,
            CommentCluster commentCluster,
            AntiCommentCluster antiCommentCluster,
            IPrefabLoader<CommentMono> commentCommentLoader,
            IPrefabLoader<AntiCommentMono> antiCommentLoader,
            ViewerNumber viewerNumber,
            ComboCounter comboCounter,
            IComboMultiplier comboMultiplier
        )
        {
            _container = container;
            _commentParamsServer = commentParamsServer;
            _commentCluster = commentCluster;
            _antiCommentCluster = antiCommentCluster;
            _commentLoader = commentCommentLoader;
            _antiCommentLoader = antiCommentLoader;
            _comboCounter = comboCounter;
            _comboMultiplier = comboMultiplier;
            _viewerNumber = viewerNumber;
        }

        void IUpdate.Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyDown(KeyCode.Space)) SpawnComment();
#endif
        }


        public void SpawnCommentByType(CommentEnum commentEnum)
        {
            if (commentEnum == CommentEnum.Normal) SpawnComment();
            else if (commentEnum == CommentEnum.Super) SpawnComment();
            else if (commentEnum == CommentEnum.Spiky) SpawnAntiComment();
        }

        void SpawnComment()
        {
            var commentPrefab = _commentLoader.Load();
            var comment = _container.Instantiate(commentPrefab, _commentParamsServer.GetSpawnedPosition(),
                Quaternion.identity, _commentParamsServer.GetCommentParent());
            comment.SetParameter(_commentParamsServer.GetRandomCommentWord()); // コメントの文章を抽選する
            _commentCluster.Add(comment);

            // 視聴者を増やす
            var multipliedAmount = (int)(_commentParamsServer.GetViewerDiffCommentNumber() * _comboMultiplier.CalculateComboMultiplier(_comboCounter.ComboCount));
            _viewerNumber.IncreaseViewer(multipliedAmount);
        }

        void SpawnAntiComment()
        {
            var commentPrefab = _antiCommentLoader.Load();
            var spawnPosition = _commentParamsServer.GetAntiSpawnedPosition();
            var comment = _container.Instantiate(commentPrefab, spawnPosition,
                Quaternion.identity, _commentParamsServer.GetCommentParent());
            comment.SetParameter(_commentParamsServer.GetRandomAntiCommentWord());　// コメントの文章を抽選する 
            _antiCommentCluster.Add(comment);

            // 視聴者を減らす
            var multipliedAmount = (int)(_commentParamsServer.GetViewerDiffAntiCommentNumber() * _comboMultiplier.CalculateComboMultiplier(_comboCounter.ComboCount));
            _viewerNumber.DecreaseViewer(multipliedAmount);
        }
    }
}