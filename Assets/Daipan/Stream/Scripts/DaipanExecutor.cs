#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.Streamer.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using UnityEngine;
using Daipan.Stream.MonoScripts;

namespace Daipan.Stream.Scripts
{
    public sealed class DaipanExecutor
    {
        readonly AntiCommentCluster _antiCommentCluster;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedValue _irritatedValue;
        readonly StreamerViewMono _streamerViewMono;
        readonly ShakeDisplayMono _shakeDisplayMono;

        public DaipanExecutor(
            IrritatedValue irritatedValue,
            EnemyCluster enemyCluster,
            AntiCommentCluster antiCommentCluster,
            StreamerViewMono streamerViewMono,
            ShakeDisplayMono shakeDisplayMono
        )
        {
            _irritatedValue = irritatedValue;
            _enemyCluster = enemyCluster;
            _antiCommentCluster = antiCommentCluster;
            _streamerViewMono = streamerViewMono;
            _shakeDisplayMono = shakeDisplayMono;
        }
        public void DaiPan()
        {
            var canDaipan = _irritatedValue.IsFull;
            if (canDaipan)
            {
                Debug.Log($"Daipan!");
                _enemyCluster.Daipaned();
                _antiCommentCluster.BlownAway();
                _streamerViewMono.Daipan();
                _shakeDisplayMono.Daipan();
                
                // 台パンしたら怒りゲージは0になる
                _irritatedValue.DecreaseValue(_irritatedValue.Value);
            }
            else
            {
                // 何もしない
                // 台パンをスカした時のアニメーションを再生するかもしれない
            }

   
        }
    }
}