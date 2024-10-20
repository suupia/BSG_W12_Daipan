#nullable enable
using Daipan.Comment.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Sound.MonoScripts;
using UnityEngine;
using Daipan.Stream.MonoScripts;
using Daipan.Streamer.MonoScripts;

namespace Daipan.Stream.Scripts
{
    public sealed class DaipanExecutor
    {
        readonly AntiCommentCluster _antiCommentCluster;
        readonly EnemyCluster _enemyCluster;
        readonly IrritatedGaugeValue _irritatedGaugeValue;
        readonly StreamerViewMono _streamerViewMono;
        readonly ShakeDisplayMono _shakeDisplayMono;
        public int DaipanCount { get; private set; }
        public DaipanExecutor(
            IrritatedGaugeValue irritatedGaugeValue,
            EnemyCluster enemyCluster,
            AntiCommentCluster antiCommentCluster,
            StreamerViewMono streamerViewMono,
            ShakeDisplayMono shakeDisplayMono
        )
        {
            _irritatedGaugeValue = irritatedGaugeValue;
            _enemyCluster = enemyCluster;
            _antiCommentCluster = antiCommentCluster;
            _streamerViewMono = streamerViewMono;
            _shakeDisplayMono = shakeDisplayMono;
        }
        public void DaiPan()
        {
            var canDaipan = _irritatedGaugeValue.IsFull;
            if (canDaipan)
            {
                Debug.Log($"Daipan!");
                _enemyCluster.Daipaned();
                _antiCommentCluster.Daipaned();
                _streamerViewMono.Daipan();
                _shakeDisplayMono.Daipan();
                DaipanCount++;
                
                // 台パンしたら怒りゲージは0になる
                _irritatedGaugeValue.Reset();
                
                SoundManager.Instance?.PlaySe(SeEnum.Daipan);
            }
            else
            {
                // 何もしない
                // 台パンをスカした時のアニメーションを再生するかもしれない
            }

   
        }
    }
}