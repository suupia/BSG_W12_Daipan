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
using Daipan.Stream.Interfaces;
using Daipan.Stream.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilder : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly IEnemyCluster _enemyCluster;
        readonly ICommentSpawner _commentSpawner;
        readonly ComboSpawner _comboSpawner;
        readonly WaveState _waveState;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly ThresholdResetCounter _playerMissedAttackCounter;
        readonly ViewerDifferenceSpawner _viewerDifferenceSpawner;
        readonly CommentParamsServer _commentParamsServer;
        readonly IComboMultiplier _comboMultiplier;
        readonly IViewerNumber _viewerNumber;

        public PlayerAttackEffectBuilder(
            IPlayerParamDataContainer playerParamDataContainer
            , ComboCounter comboCounter
            , IEnemyCluster enemyCluster
            , ICommentSpawner commentSpawner
            , ComboSpawner comboSpawner
            , WaveState waveState
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , ViewerDifferenceSpawner viewerDifferenceSpawner
            , CommentParamsServer commentParamManager
            , IComboMultiplier comboMultiplier
            , IViewerNumber viewerNumber
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
            _viewerDifferenceSpawner = viewerDifferenceSpawner;
            _commentParamsServer = commentParamManager;
            _comboMultiplier = comboMultiplier;
            _viewerNumber = viewerNumber;
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
                    int spawnCount = 0;
                    Debug.Log($"OnHit");
                    bool isSpawned0 = AttackEnemy(
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
                    bool isSpawned1 = SpawnAntiComment(args, _commentSpawner, _playerAntiCommentParamData, _waveState);
                    spawnCount += isSpawned0 ? 1 : 0;
                    spawnCount += isSpawned1 ? 1 : 0;
                    SpawnViewerDifference(args, _viewerDifferenceSpawner, _commentParamsServer, _comboMultiplier, _viewerNumber, _comboCounter, spawnCount);
                };
                return effect;
            };
        }


        static bool AttackEnemy(
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
                bool isAntiCommentSpawned = false;
                Debug.Log(
                    $"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                comboCounter.ResetCombo();
                playerMissedAttackCounter.CountUp();
                if (playerMissedAttackCounter.IsOverThreshold)
                {
                    commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                    isAntiCommentSpawned = true;
                }
                if (args.EnemyMono != null)
                {
                    playerAttackEffectMono.Defenced();
                    SoundManager.Instance?.PlaySe(SeEnum.AttackDeflect);
                }

                return isAntiCommentSpawned;
            }


            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (playerViewMono.playerColor == playerColor)
                    playerViewMono.Attack();
            }

            return false;
        }

        static bool SpawnAntiComment(
            OnHitEventArgs args
            , ICommentSpawner commentSpawner
            , IPlayerAntiCommentParamData playerAntiCommentParamData
            , WaveState waveState
            )
        {
            if (args.IsTargetEnemy) return false;
            if (args.EnemyMono != null && args.EnemyMono.EnemyEnum.IsTotem() == true) return false;  // TotemはOnAttackedで判定している

            var spawnPercent = playerAntiCommentParamData.GetAntiCommentPercentOnMissAttacks(waveState.CurrentWaveIndex);

            if (spawnPercent / 100f > Random.value)
            {
                commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                Debug.Log($"ANTI SPAWN");
                return true;
            }
            return false;

        }
        static void SpawnViewerDifference(
            OnHitEventArgs args
            , ViewerDifferenceSpawner viewerDifferenceSpawner
            , CommentParamsServer commentParamsServer
            , IComboMultiplier comboMultiplier
            , IViewerNumber viewerNumber
            , ComboCounter comboCounter
            , int spawnCount
        )
        {
            if (args.IsTargetEnemy) return;
            if (args.EnemyMono == null) return;
            int diff = (int)(commentParamsServer.GetViewerDiffAntiCommentNumber() * comboMultiplier.CalculateComboMultiplier(comboCounter.ComboCount)) * spawnCount;
            diff = Math.Min(diff, viewerNumber.Number);
            viewerDifferenceSpawner.SpawnViewer(-diff, args.EnemyMono.Transform.position);
        }
    }
}