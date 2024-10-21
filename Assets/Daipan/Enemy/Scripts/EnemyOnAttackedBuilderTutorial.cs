#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyOnAttackedBuilderTutorial
    {
        readonly IrritatedGaugeValue _irritatedGaugeValue;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly ComboCounter _comboCounter;
        readonly CommentSpawner _commentSpawner;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly WaveState _waveState;

        public EnemyOnAttackedBuilderTutorial(
            IrritatedGaugeValue irritatedGaugeValue
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , ComboCounter comboCounter
            , CommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
        )
        {
            _irritatedGaugeValue = irritatedGaugeValue;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _comboCounter = comboCounter;
            _commentSpawner = commentSpawner;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _waveState = waveState;
        }

        public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum)
        {
            if (enemyEnum.IsSpecial() == true)
                return new EnemySpecialOnAttacked(enemyEnum, _irritatedGaugeValue, _enemyLevelDesignParamData);
            if (enemyEnum == EnemyEnum.Totem2 || enemyEnum == EnemyEnum.Totem3) return BuildTotemOnAttack(enemyEnum);
            return new EnemyNormalOnAttacked();
        }

        EnemyTotemOnAttackedTutorial BuildTotemOnAttack(EnemyEnum enemyEnum)
        {
            return enemyEnum switch
            {
                // todo : 一旦Viewとの兼ね合いで色を固定
                EnemyEnum.Totem2 => new EnemyTotemOnAttackedTutorial(new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue }),
                EnemyEnum.Totem3 => new EnemyTotemOnAttackedTutorial(new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow } ),
                _ => throw new System.ArgumentException("Invalid totem type")
            };
        }
    }

}