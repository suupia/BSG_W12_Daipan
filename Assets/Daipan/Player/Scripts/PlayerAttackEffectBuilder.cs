#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectBuilder
    {
        readonly PlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly EnemyCluster _enemyCluster;
        
        public PlayerAttackEffectBuilder(PlayerParamDataContainer playerParamDataContainer, ComboCounter comboCounter, EnemyCluster enemyCluster)
        {
            _playerParamDataContainer = playerParamDataContainer;
            _comboCounter = comboCounter;
            _enemyCluster = enemyCluster;
        }

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono ,List<AbstractPlayerViewMono> playerViewMonos, PlayerColor playerColor)
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                if (args.IsTargetEnemy)
                {
                    OnAttackEnemy(_playerParamDataContainer, playerViewMonos, playerColor, args.EnemyMono);
                    _comboCounter.IncreaseCombo();
                }
                else
                {
                    Debug.Log($"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                    _comboCounter.ResetCombo();
                    // todo : 特攻処理を書く
                }
            };
            return effect;
        }

        static void OnAttackEnemy(PlayerParamDataContainer playerParamDataContainer,
            List<AbstractPlayerViewMono?> playerViewMonos,
            PlayerColor playerColor, EnemyMono? enemyMono)
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            if (enemyMono == null) return;
            var targetEnemies = PlayerAttackModule.GetTargetEnemyEnum(playerColor);

            if (targetEnemies.Contains(enemyMono.EnemyEnum))
            {
                Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
                enemyMono.CurrentHp -= playerParamDataContainer.GetPlayerParamData(playerColor).GetAttack();

                // Animation
                foreach (var playerViewMono in playerViewMonos)
                {
                    if (playerViewMono == null) continue;
                    if(playerViewMono.playerColor == playerColor)
                        playerViewMono.Attack();
                }
            }
            else
            {
                Debug.Log($"攻撃対象が{enemyMono.EnemyEnum}ではないよ");
            }
        }
    }
}