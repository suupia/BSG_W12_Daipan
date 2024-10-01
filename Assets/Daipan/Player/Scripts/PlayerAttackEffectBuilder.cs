#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Sound.MonoScripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilder : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly EnemyCluster _enemyCluster;
        readonly CommentSpawner _commentSpawner;
        readonly ComboSpawner _comboSpawner;
        readonly WaveState _waveState;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly ThresholdResetCounter _playerMissedAttackCounter;

        public PlayerAttackEffectBuilder(
            IPlayerParamDataContainer playerParamDataContainer
            ,ComboCounter comboCounter
            ,EnemyCluster enemyCluster
            ,CommentSpawner commentSpawner
            ,ComboSpawner comboSpawner
            ,WaveState waveState
            ,IPlayerAntiCommentParamData playerAntiCommentParamData
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
            _commentSpawner = commentSpawner;
            _comboSpawner = comboSpawner;
            _waveState = waveState;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _playerMissedAttackCounter =
                new ThresholdResetCounter(playerAntiCommentParamData.GetMissedAttackCountForAntiComment());
        }

        public PlayerAttackEffectMono Build
        (
            PlayerAttackEffectMono effect
            , PlayerMono playerMono
            , List<AbstractPlayerViewMono?> playerViewMonos
            , PlayerColor playerColor
            )
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                AttackEnemy(
                    _playerParamDataContainer
                    , effect
                    , playerViewMonos
                    , playerColor
                    , args
                    ,_comboCounter
                    , _playerMissedAttackCounter
                    , _commentSpawner
                    , _comboSpawner
                    );
                SpawnAntiComment(args, _commentSpawner, _playerAntiCommentParamData,_waveState);
            };
            return effect;
        }


        static void AttackEnemy(
            IPlayerParamDataContainer playerParamDataContainer
            , PlayerAttackEffectMono playerAttackEffectMono
            , List<AbstractPlayerViewMono?> playerViewMonos
            , PlayerColor playerColor
            , OnHitEventArgs args
            , ComboCounter comboCounter
            , ThresholdResetCounter playerMissedAttackCounter
            , CommentSpawner commentSpawner
            , ComboSpawner comboSpawner
        )
        {
            if (args.IsTargetEnemy && args.EnemyMono != null)
            {
                Debug.Log($"EnemyType: {args.EnemyMono.EnemyEnum}を攻撃");
                // 敵を攻撃
                var playerParamData = playerParamDataContainer.GetPlayerParamData(playerColor);
                var beforeHp = args.EnemyMono.Hp.Value; 
                PlayerAttackModule.Attack(args.EnemyMono, playerParamData);
                var afterHp = args.EnemyMono.Hp.Value;

                //  HPに変化があれば、コンボ増加（ただし、Totemの判定はOnAttackedで行っている）
                if (Math.Abs(beforeHp - afterHp) > double.Epsilon && args.EnemyMono.EnemyEnum.IsTotem() != true)
                    comboCounter.IncreaseCombo();
            
                // コンボを表示
                comboSpawner.SpawnCombo(comboCounter.ComboCount, args.EnemyMono.transform.position);
                
                if(args.EnemyMono.EnemyEnum.IsTotem() != true) SoundManager.Instance?.PlaySe(SeEnum.Attack);
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

        static void SpawnAntiComment(
            OnHitEventArgs args
            ,CommentSpawner commentSpawner
            ,IPlayerAntiCommentParamData playerAntiCommentParamData
            ,WaveState waveState
            )
        {
            if (args.IsTargetEnemy) return;
            if (args.EnemyMono != null && args.EnemyMono.EnemyEnum.IsTotem() == true) return;  // TotemはOnAttackedで判定している
            
            var spawnPercent = playerAntiCommentParamData.GetAntiCommentPercentOnMissAttacks(waveState.CurrentWaveIndex);
            
            if (spawnPercent / 100f > Random.value)
            {
                commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
            }
           
        }
    }
}