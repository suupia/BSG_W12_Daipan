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
using Daipan.Sound.Interfaces;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyOnAttackedBuilder
    {
        readonly IrritatedValue _irritatedValue;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly ComboCounter _comboCounter;
        readonly CommentSpawner _commentSpawner;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly WaveState _waveState;
        readonly ISoundManager _soundManager;

        public EnemyOnAttackedBuilder(
            IrritatedValue irritatedValue
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , ComboCounter comboCounter
            , CommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
            , ISoundManager soundManager
        )
        {
            _irritatedValue = irritatedValue;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _comboCounter = comboCounter;
            _commentSpawner = commentSpawner;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _waveState = waveState;
            _soundManager = soundManager;
        }

        public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum)
        {
            if (enemyEnum.IsSpecial() == true)
                return new EnemySpecialOnAttacked(enemyEnum, _irritatedValue, _enemyLevelDesignParamData);
            if (enemyEnum == EnemyEnum.Totem2 || enemyEnum == EnemyEnum.Totem3) return BuildTotemOnAttack(enemyEnum);
            return new EnemyNormalOnAttacked();
        }

        EnemyTotemOnAttacked BuildTotemOnAttack(EnemyEnum enemyEnum)
        {
            return enemyEnum switch
            {
                // todo : 一旦Viewとの兼ね合いで色を固定
                EnemyEnum.Totem2 => new EnemyTotemOnAttacked(_comboCounter, _commentSpawner, _playerAntiCommentParamData,_waveState,
                    new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue }, _soundManager),
                EnemyEnum.Totem3 => new EnemyTotemOnAttacked(_comboCounter,  _commentSpawner, _playerAntiCommentParamData,_waveState,
                    new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow }, _soundManager),
                _ => throw new System.ArgumentException("Invalid totem type")
            };
        }

        static List<PlayerColor> RandomPlayerColor(int length)
        {
            var colors = new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow };
            colors.Shuffle();
            return colors.Take(length).ToList();
        }
    }

    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the element order of the specified list.
        /// </summary>
        public static void Shuffle<T>(this IList<T> list)
        {
            var count = list.Count;
            var last = count - 1;
            for (var i = 0; i < last; ++i)
            {
                var rand = UnityEngine.Random.Range(i, count);
                (list[i], list[rand]) = (list[rand], list[i]);
            }
        }
    }
}