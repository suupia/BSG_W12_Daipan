#nullable enable
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
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilder : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly EnemyCluster _enemyCluster;
        readonly CommentSpawner _commentSpawner;
        readonly EnemyTotemOnAttack _enemyTotemOnAttack;
        readonly EnemySpecialOnAttack _enemySpecialOnAttack;
        readonly WaveState _waveState;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;

        EnemyTotemOnAttackNew _totemOnAttack2;
        EnemyTotemOnAttackNew _totemOnAttack3;

        public PlayerAttackEffectBuilder(
            IPlayerParamDataContainer playerParamDataContainer
            ,ComboCounter comboCounter
            ,EnemyCluster enemyCluster
            ,CommentSpawner commentSpawner
            ,EnemyTotemOnAttack enemyTotemOnAttack
            ,EnemySpecialOnAttack enemySpecialOnAttack
            ,WaveState waveState
            ,IPlayerAntiCommentParamData playerAntiCommentParamData
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
            _commentSpawner = commentSpawner;
            _enemyTotemOnAttack = enemyTotemOnAttack;
            _enemySpecialOnAttack = enemySpecialOnAttack;
            _waveState = waveState;
            _playerAntiCommentParamData = playerAntiCommentParamData;
        }

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            BuildEnemyTotemOnAttacked();
            
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                AttackEnemy(_playerParamDataContainer, playerViewMonos, playerColor, args,_comboCounter, _enemyTotemOnAttack,_enemySpecialOnAttack ,_totemOnAttack2,_totemOnAttack3 );
                SpawnAntiComment(args, _commentSpawner, _playerAntiCommentParamData,_waveState);
            };
            return effect;
        }

        void BuildEnemyTotemOnAttacked()
        {
            _totemOnAttack2 = new EnemyTotemOnAttackNew(new List<PlayerColor> {PlayerColor.Red, PlayerColor.Blue});
            _totemOnAttack3 = new EnemyTotemOnAttackNew(new List<PlayerColor> {PlayerColor.Red, PlayerColor.Blue, PlayerColor.Yellow});
        }
        

        static void AttackEnemy(
            IPlayerParamDataContainer playerParamDataContainer
            , List<AbstractPlayerViewMono?> playerViewMonos
            , PlayerColor playerColor
            , OnHitEventArgs args
            , ComboCounter comboCounter
            , EnemyTotemOnAttack totemOnAttack
            , EnemySpecialOnAttack enemySpecialOnAttack
            , EnemyTotemOnAttackNew enemyTotemOnAttackNew2
            , EnemyTotemOnAttackNew enemyTotemOnAttackNew3
        )
        {
            if (args.IsTargetEnemy && args.EnemyMono != null)
            {
                Debug.Log($"EnemyType: {args.EnemyMono.EnemyEnum}を攻撃");
                // 敵を攻撃
                var playerParamData = playerParamDataContainer.GetPlayerParamData(playerColor);
                var hpBuffer = args.EnemyMono.EnemyEnum switch
                {
                    EnemyEnum.Totem2 => enemyTotemOnAttackNew2.OnAttacked(args.EnemyMono.Hp, playerParamData),
                    EnemyEnum.Totem3 => enemyTotemOnAttackNew3.OnAttacked(args.EnemyMono.Hp, playerParamData),
                    EnemyEnum.Special => enemySpecialOnAttack.OnAttacked(args.EnemyMono.Hp,args.EnemyMono.EnemyEnum, playerParamData),
                        _ => PlayerAttackModule.Attack(args.EnemyMono.Hp, playerParamData)
                };
                //  HPに変化があれば、コンボ増加
                if (args.EnemyMono.Hp.Value != hpBuffer.Value) comboCounter.IncreaseCombo();
                args.EnemyMono.Hp = hpBuffer;
            }
            else
            {
                Debug.Log(
                    $"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                comboCounter.ResetCombo();
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
            
            var spawnPercent = playerAntiCommentParamData.GetAntiCommentPercentOnMissAttacks(waveState.CurrentWave);
            
            if (spawnPercent / 100f > Random.value)
            {
                commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
            }
           
        }
    }
}