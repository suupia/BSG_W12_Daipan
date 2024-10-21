#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerOnAttacked : IPlayerOnAttacked
    {
        readonly IrritatedGaugeValue _irritatedGaugeValue;
        readonly ThresholdResetCounter _playerAttackedCounter;
        readonly CommentSpawner _commentSpawner;
        List<AbstractPlayerViewMono?>? _playerViewMonos; 
        public PlayerOnAttacked
        (
            IrritatedGaugeValue irritatedGaugeValue
            , CommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
        )
        {
            _irritatedGaugeValue = irritatedGaugeValue;
            _commentSpawner = commentSpawner;
            _playerAttackedCounter = new ThresholdResetCounter(playerAntiCommentParamData.GetAntiCommentThreshold());
        }

        public void SetPlayerViews(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _playerViewMonos = playerViewMonos;
        }

        public Hp OnAttacked(Hp hp, IEnemyParamData enemyParamData)
        {
            // イライラゲージ
            _irritatedGaugeValue.IncreaseValue(enemyParamData.GetIncreaseIrritatedValueOnAttack());
            
            // アンチコメント
            _playerAttackedCounter.CountUp();
            if (_playerAttackedCounter.IsOverThreshold)
                _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
            
            // view
            if (_playerViewMonos == null)
            {
                Debug.LogWarning("PlayerViewMonos is null");
                return new Hp(hp.Value - enemyParamData.GetAttackAmount());
            }
            foreach (var playerViewMono in _playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (PlayerAttackModule.GetTargetEnemyEnum(playerViewMono.playerColor).Contains(enemyParamData.GetEnemyEnum()))
                    playerViewMono.Damage();
            }            
            return new Hp(hp.Value - enemyParamData.GetAttackAmount());
        }

    } 
}

