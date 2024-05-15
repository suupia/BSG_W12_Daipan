#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using UnityEngine;

namespace Daipan.Stream.Scripts
{
    public class DaipanExecutor
    {
        readonly DaipanParameter _daipanParameter;
        readonly ViewerNumber _viewerNumber;
        readonly StreamStatus _streamStatus;
        readonly IrritatedValue _irritatedValue;
        readonly EnemyCluster _enemyCluster;
        readonly CommentCluster _commentCluster;

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
                // 敵、コメント欄共に何も起きない
                Debug.Log($"Do nothing");
            } else if (_irritatedValue.Value < 100)
            {
                // 通常の敵が吹き飛ぶ
                // コメントは確率で吹き飛ぶ
                Debug.Log($"Blow normal enemy");
                Debug.Log($"Blow comment by probability");
                float blowAwayProbability = 0.5f;
                _commentCluster.BlownAway(blowAwayProbability); 
            }
            else
            {
                // 全ての敵が吹き飛ぶ
                // 全てのコメントが吹き飛ぶ
                Debug.Log($"Blow all enemy");
                Debug.Log($"Blow all comment");
                
                _enemyCluster.BlownAway();
                _commentCluster.BlownAway();
            }
            
            // 台パンしたら怒りゲージは0になる
            _irritatedValue.DecreaseValue(_irritatedValue.Value);
        }
    }
}