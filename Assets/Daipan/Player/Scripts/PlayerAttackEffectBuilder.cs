#nullable enable
using System.Collections.Generic;
using System.Linq;
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
    public class PlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly EnemyCluster _enemyCluster;
        readonly CommentSpawner _commentSpawner;
        readonly EnemyTotemOnAttack _enemyTotemOnAttack;

        public PlayerAttackEffectBuilder(
            IPlayerParamDataContainer playerParamDataContainer
            ,ComboCounter comboCounter
            ,EnemyCluster enemyCluster
            ,CommentSpawner commentSpawner
            ,EnemyTotemOnAttack enemyTotemOnAttack
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
            _commentSpawner = commentSpawner;
            _enemyTotemOnAttack = enemyTotemOnAttack;
        }

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                AttackEnemy(_playerParamDataContainer, playerViewMonos, playerColor, args.EnemyMono,_enemyTotemOnAttack );
                UpdateCombo(_comboCounter, playerColor, args);
                SpawnAntiComment(args, _commentSpawner);
            };
            return effect;
        }

        static void UpdateCombo(
            ComboCounter comboCounter
            ,PlayerColor playerColor
            ,OnHitEventArgs args
        )
        {
            if (args.IsTargetEnemy)
            {
                comboCounter.IncreaseCombo();
            }
            else
            {
                Debug.Log(
                    $"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                comboCounter.ResetCombo();
            }
        }

        static void AttackEnemy(IPlayerParamDataContainer playerParamDataContainer
            ,List<AbstractPlayerViewMono?> playerViewMonos
            ,PlayerColor playerColor
            ,EnemyMono? enemyMono
            ,EnemyTotemOnAttack totemOnAttack
            )
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            if (enemyMono == null) return;


            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            if (PlayerAttackModule.GetTargetEnemyEnum(playerColor).Contains(enemyMono.EnemyEnum))
            {
                // 敵を攻撃
                var playerParamData = playerParamDataContainer.GetPlayerParamData(playerColor);
                enemyMono.Hp = enemyMono.EnemyEnum switch 
                {
                    EnemyEnum.Totem => totemOnAttack.OnAttacked(enemyMono.Hp, playerParamData),
                    _ => PlayerAttackModule.Attack(enemyMono.Hp,playerParamData)
                    // 敵が特攻攻撃をしてくる
                    // todo: 一旦はなし
                    // enemyMono.SuicideAttack(playerMono); 
                };

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
            )
        {
            if (args.IsTargetEnemy) return;
            
           //  commentSpawner.SpawnCommentByType(CommentEnum.Spiky); 
        }
    }
}