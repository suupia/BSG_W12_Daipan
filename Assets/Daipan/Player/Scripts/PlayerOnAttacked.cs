#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerOnAttacked : IPlayerOnAttacked
    {
        readonly IrritatedValue _irritatedValue;
        readonly ThresholdResetCounter _thresholdResetCounter;
        readonly CommentSpawner _commentSpawner;
        List<AbstractPlayerViewMono?>? _playerViewMonos; 
        public PlayerOnAttacked
        (
            IrritatedValue irritatedValue
            , ThresholdResetCounter thresholdResetCounter
            , CommentSpawner commentSpawner
        )
        {
            _irritatedValue = irritatedValue;
            _thresholdResetCounter = thresholdResetCounter;
            _commentSpawner = commentSpawner;
        }

        public void SetPlayerViews(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _playerViewMonos = playerViewMonos;
        }

        public Hp OnAttacked(Hp hp, IEnemyParamData enemyParamData)
        {
            // イライラゲージ
            _irritatedValue.IncreaseValue(enemyParamData.GetAttackAmount());
            
            // アンチコメント
            _thresholdResetCounter.CountUp();
            if (_thresholdResetCounter.IsOverThreshold)
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

