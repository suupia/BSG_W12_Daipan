#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Interfaces;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.LevelDesign.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.MonoScripts;
using Daipan.Player.Scripts;
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using Daipna.StreamerNet.Scripts;

namespace Daipan.Enemy.Scripts
{
    public class EnemyOnAttackedBuilderNetwork
    {
        readonly IIrritatedGaugeValue _irritatedGaugeValue;
        readonly EnemyLevelDesignParamData _enemyLevelDesignParamData;
        readonly ComboCounter _comboCounter;
        readonly ICommentSpawner _commentSpawner;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly WaveState _waveState;
        readonly EnemyClusterNetwork _enemyClusterNetwork;
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;

        public EnemyOnAttackedBuilderNetwork(
            IIrritatedGaugeValue irritatedGaugeValue
            , EnemyLevelDesignParamData enemyLevelDesignParamData
            , ComboCounter comboCounter
            , ICommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
            , EnemyClusterNetwork enemyClusterNetwork
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
        )
        {
            _irritatedGaugeValue = irritatedGaugeValue;
            _enemyLevelDesignParamData = enemyLevelDesignParamData;
            _comboCounter = comboCounter;
            _commentSpawner = commentSpawner;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _waveState = waveState;
            _enemyClusterNetwork = enemyClusterNetwork;
            _rpcReceiverNetWrapper = rpcReceiverNetWrapper;
        }

        public IEnemyOnAttacked SwitchEnemyOnAttacked(EnemyEnum enemyEnum, IEnemyMono enemyMono)
        {
            if (enemyEnum.IsSpecial() == true)
                return new EnemySpecialOnAttacked(enemyEnum, _irritatedGaugeValue, _enemyLevelDesignParamData);
            if (enemyEnum == EnemyEnum.Totem2 || enemyEnum == EnemyEnum.Totem3) return BuildTotemOnAttack(enemyEnum, enemyMono);
            return new EnemyNormalOnAttacked();
        }

        EnemyTotemOnAttackedNetwork BuildTotemOnAttack(EnemyEnum enemyEnum, IEnemyMono enemyMono)
        {
            return enemyEnum switch
            {
                // todo : 一旦Viewとの兼ね合いで色を固定
                EnemyEnum.Totem2 => new EnemyTotemOnAttackedNetwork(enemyMono, _comboCounter, _commentSpawner, _playerAntiCommentParamData, _waveState,
                    new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue }, _enemyClusterNetwork, _rpcReceiverNetWrapper),
                EnemyEnum.Totem3 => new EnemyTotemOnAttackedNetwork(enemyMono, _comboCounter, _commentSpawner, _playerAntiCommentParamData, _waveState,
                    new List<PlayerColor> { PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow }, _enemyClusterNetwork, _rpcReceiverNetWrapper),
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
}