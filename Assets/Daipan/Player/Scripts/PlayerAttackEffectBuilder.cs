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
        readonly ViewerNumber _viewerNumber;
        readonly CommentSpawner _commentSpawner;
        readonly CommentParamsServer _commentParamsServer;

        public PlayerAttackEffectBuilder(
            IPlayerParamDataContainer playerParamDataContainer,
            ComboCounter comboCounter,
            EnemyCluster enemyCluster,
            ViewerNumber viewerNumber,
            CommentSpawner commentSpawner,
            CommentParamsServer commentParamsServer
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
            _viewerNumber = viewerNumber;
            _commentSpawner = commentSpawner;
            _commentParamsServer = commentParamsServer;
        }

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                OnAttackEnemy(_playerParamDataContainer, playerMono, playerViewMonos, playerColor, args.EnemyMono);
                OnProcessCombo(_comboCounter, playerColor, args);
            };
            return effect;
        }

        static void OnProcessCombo(
            ComboCounter comboCounter,
            PlayerColor playerColor,
            OnHitEventArgs args
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

        static void OnAttackEnemy(IPlayerParamDataContainer playerParamDataContainer,
            PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos,
            PlayerColor playerColor, EnemyMono? enemyMono)
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            // [Precondition]
            if (enemyMono == null) return;

            // [Main]
            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            if (PlayerAttackModule.GetTargetEnemyEnum(playerColor).Contains(enemyMono.EnemyEnum))
                // 敵を攻撃
                enemyMono.CurrentHp -= playerParamDataContainer.GetPlayerParamData(playerColor).GetAttack();
            else
                // 敵が特攻攻撃をしてくる
                // todo: 一旦はなし
                // enemyMono.SuicideAttack(playerMono);

            // Animation
            foreach (var playerViewMono in playerViewMonos)
            {
                if (playerViewMono == null) continue;
                if (playerViewMono.playerColor == playerColor)
                    playerViewMono.Attack();
            }
        }
    }
}