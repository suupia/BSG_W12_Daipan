#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public class DaipanExecutor
    {
        readonly CommentCluster _commentCluster;
        readonly DaipanParameter _daipanParameter;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly StreamStatus _streamStatus;
        readonly ViewerNumber _viewerNumber;

        public DaipanExecutor(
            DaipanParameter daipanParameter,
            ViewerNumber viewerNumber,
            StreamStatus streamStatus,
            IrritatedValue irritatedValue,
            EnemyCluster enemyCluster,
            CommentCluster commentCluster
        )
        {
            _daipanParameter = daipanParameter;
            _viewerNumber = viewerNumber;
            _streamStatus = streamStatus;
            _irritatedValue = irritatedValue;
            _enemyCluster = enemyCluster;
            _commentCluster = commentCluster;
        }

        bool IsExciting => _streamStatus.IsExcited;

        public void DaiPan()
        {
            // if (IsExciting)
            //     _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberWhenExciting);
            // else
            //     _viewerNumber.IncreaseViewer(_daipanParameter.increaseNumberByDaipan);

            if (_irritatedValue.Value < 50)
            {
                Debug.Log("Do nothing");
            }
            else if (_irritatedValue.Value < 100)
            {
                Debug.Log("Blow normal enemy");
                Debug.Log("Blow comment by probability");
                var blowAwayProbability = 0.5f;
                // _enemyCluster.BlownAway(blowAwayProbability);
                _enemyCluster.BlownAway(enemyEnum => !enemyEnum.IsBoss);
                _commentCluster.BlownAway(blowAwayProbability);
            }
            else
            {
                Debug.Log("Blow all enemy");
                Debug.Log("Blow all comment");

                _enemyCluster.BlownAway();
                // _commentCluster.BlownAway();  // すべてを吹き飛ばす
                _commentCluster.BlownAway(commentMono => !commentMono.IsSuperComment);  // SuperComment以外を吹き飛ばす
            }

            // 台パンしたら怒りゲージは0になる
            _irritatedValue.DecreaseValue(_irritatedValue.Value);
        }
    }
}