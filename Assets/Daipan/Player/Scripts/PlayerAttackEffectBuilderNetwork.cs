#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Interfaces;
using Daipan.Comment.Scripts;
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Scripts;
using Daipna.StreamerNet.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilderNetwork : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly IEnemyCluster _enemyCluster;
        readonly ICommentSpawner _commentSpawner;
        readonly ComboSpawner _comboSpawner;
        readonly WaveState _waveState;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly ThresholdResetCounter _playerMissedAttackCounter;
        readonly RpcReceiverNetWrapper _rpcReceiverNetWrapper;

        public PlayerAttackEffectBuilderNetwork(
            IPlayerParamDataContainer playerParamDataContainer
            , ComboCounter comboCounter
            , IEnemyCluster enemyCluster
            , ICommentSpawner commentSpawner
            , ComboSpawner comboSpawner
            , WaveState waveState
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , RpcReceiverNetWrapper rpcReceiverNetWrapper
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
            _commentSpawner = commentSpawner;
            _comboSpawner = comboSpawner;
            _waveState = waveState;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _playerMissedAttackCounter = new ThresholdResetCounter(playerAntiCommentParamData.GetMissedAttackCountForAntiComment());
            _rpcReceiverNetWrapper = rpcReceiverNetWrapper;
        }

        public Func<IPlayerAttackEffectMono, IPlayerAttackEffectMono> Build
        (
            IMonoBehaviour playerMono
            , List<AbstractPlayerViewMono?> playerViewMonos
            , PlayerColor playerColor
            )
        {
            return effect =>
            {
                effect.Initialize(_playerParamDataContainer);
                effect.SetUp(playerColor, () => _enemyCluster.NearestEnemy(playerMono.Transform.position));
                effect.OnHit += (sender, args) =>
                {
                    Debug.Log($"OnHit");
                    AttackEnemy(
                        _playerParamDataContainer
                        , effect
                        , playerViewMonos
                        , playerColor
                        , args
                        , _comboCounter
                        , _playerMissedAttackCounter
                        , _commentSpawner
                        , _comboSpawner
                    );
                    MakeAntiFever(args);
                };
                return effect;
            };
        }


        static void AttackEnemy(
            IPlayerParamDataContainer playerParamDataContainer
            , IPlayerAttackEffectMono playerAttackEffectMono
            , List<AbstractPlayerViewMono?> playerViewMonos
            , PlayerColor playerColor
            , OnHitEventArgs args
            , ComboCounter comboCounter
            , ThresholdResetCounter playerMissedAttackCounter
            , ICommentSpawner commentSpawner
            , ComboSpawner comboSpawner
        )
        {
            if (args.IsTargetEnemy && args.EnemyMono != null)
            {
                Debug.Log($"EnemyType: {args.EnemyMono.EnemyEnum}を攻撃");
                // 敵を攻撃
                var playerParamData = playerParamDataContainer.GetPlayerParamData(playerColor);
                PlayerAttackModule.Attack(args.EnemyMono, playerParamData);

                if (args.EnemyMono.EnemyEnum.IsTotem() != true) SoundManager.Instance?.PlaySe(SeEnum.Attack);
            }
            else
            {
                Debug.Log(
                    $"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                comboCounter.ResetCombo();
                playerMissedAttackCounter.CountUp();
                if (playerMissedAttackCounter.IsOverThreshold) commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                if (args.EnemyMono != null)
                {
                    playerAttackEffectMono.Defenced();
                    SoundManager.Instance?.PlaySe(SeEnum.AttackDeflect);
                }

                return;
            }


            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (playerViewMono.playerColor == playerColor)
                    playerViewMono.Attack();
            }
        }

        void MakeAntiFever(OnHitEventArgs args)
        {
            if (args.EnemyMono == null) return;
            if (args.IsTargetEnemy) return;

            _rpcReceiverNetWrapper.RpcReceiverNet.SetFeverRPC();
        }


    }
}