#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public sealed class DaipanExecutor
    {
        readonly CommentCluster _commentCluster;
        readonly AntiCommentCluster _antiCommentCluster;
        readonly DaipanParam _daipanParam;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly StreamStatus _streamStatus;
        readonly ViewerNumber _viewerNumber;

        public DaipanExecutor(
            DaipanParam daipanParam,
            ViewerNumber viewerNumber,
            StreamStatus streamStatus,
            IrritatedValue irritatedValue,
            EnemyCluster enemyCluster,
            CommentCluster commentCluster,
            AntiCommentCluster antiCommentCluster
        )
        {
            _daipanParam = daipanParam;
            _viewerNumber = viewerNumber;
            _streamStatus = streamStatus;
            _irritatedValue = irritatedValue;
            _enemyCluster = enemyCluster;
            _commentCluster = commentCluster;
            _antiCommentCluster = antiCommentCluster;
        }

        bool IsExciting => _streamStatus.IsExcited;

        public void DaiPan()
        {
            int irritatedPhase = _irritatedValue.GetIrritatedPhase();

            if(irritatedPhase == 0)
            {
                Debug.Log("Do nothing");
            }
            else if (irritatedPhase == 1)
            {
                Debug.Log("Blow normal enemy");
                Debug.Log("Blow comment by probability");
                var blowAwayProbability = 0.5f;
                // _enemyCluster.BlownAway(blowAwayProbability);
                _enemyCluster.Daipaned(enemyEnum => enemyEnum.IsBoss() != false);
                _commentCluster.BlownAway(blowAwayProbability);
                _antiCommentCluster.BlownAway(blowAwayProbability);
            }
            else if (irritatedPhase == 2)
            {
                Debug.Log("Blow normal enemy");
                Debug.Log("Blow comment by probability");
                var blowAwayProbability = 0.5f;
                // _enemyCluster.BlownAway(blowAwayProbability);
                _enemyCluster.Daipaned(enemyEnum => enemyEnum.IsBoss() != false);
                _commentCluster.BlownAway(blowAwayProbability);
                _antiCommentCluster.BlownAway(blowAwayProbability);
            }
            else if (irritatedPhase == 3)
            {
                Debug.Log("Blow normal enemy");
                Debug.Log("Blow comment by probability");
                var blowAwayProbability = 0.5f;
                // _enemyCluster.BlownAway(blowAwayProbability);
                _enemyCluster.Daipaned(enemyEnum => enemyEnum.IsBoss() != true);
                _commentCluster.BlownAway(blowAwayProbability);
                _antiCommentCluster.BlownAway(blowAwayProbability);
            }
            else
            {
                Debug.Log("Blow all enemy");
                Debug.Log("Blow all comment");
                _enemyCluster.Daipaned();
                _commentCluster.BlownAway(commentMono =>
                    commentMono.CommentEnum != CommentEnum.Super); // SuperComment以外を吹き飛ばす
                _antiCommentCluster.BlownAway();
            }

            // 台パンしたら怒りゲージは0になる
            _irritatedValue.DecreaseValue(_irritatedValue.Value);
        }
    }
}