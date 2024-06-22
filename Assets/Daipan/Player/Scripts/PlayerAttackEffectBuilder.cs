#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Comment.Scripts;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Comment.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.MonoScripts;
using Daipan.Stream.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectBuilder
    {
        readonly PlayerParamDataContainer _playerParamDataContainer;
        readonly ComboCounter _comboCounter;
        readonly EnemyCluster _enemyCluster;
        readonly ViewerNumber _viewerNumber;
        readonly CommentSpawner _commentSpawner;
        readonly CommentParamsServer _commentParamsServer;
        
        public PlayerAttackEffectBuilder(
            PlayerParamDataContainer playerParamDataContainer,
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

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono ,List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                if (args.IsTargetEnemy)
                {
                    OnAttackEnemy(_playerParamDataContainer, playerViewMonos, playerColor, args.EnemyMono);
                    _comboCounter.IncreaseCombo();
                    
                    // 視聴者数が一定数以上の時、コメントを生成する
                    var commentParam = _commentParamsServer.GetCommentParamDependOnViewer();
                    if (commentParam.viewerAmount < _viewerNumber.Number)
                    {
                        for (int i = 0; i < commentParam.commentAmount; i++)
                        {
                            _commentSpawner.SpawnCommentByType(CommentEnum.Normal);
                        }
                    }
                }
                else
                {
                    Debug.Log($"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                    _comboCounter.ResetCombo();
                    // todo : 特攻処理を書く
                    
                    // 視聴者数が一定数以上の時、アンチコメントを生成する
                    var commentParam = _commentParamsServer.GetCommentParamDependOnViewer();
                    if (commentParam.viewerAmount < _viewerNumber.Number)
                    {
                        for (int i = 0; i < commentParam.commentAmount; i++)
                        {
                            _commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                        }
                    }
                }
            };
            return effect;
        }
        
        static void OnProcessCombo(
            ComboCounter comboCounter,
            PlayerParamDataContainer playerParamDataContainer,
            List<AbstractPlayerViewMono?> playerViewMonos,
            PlayerColor playerColor,
            OnHitEventArgs args
            )
        {
            if (args.IsTargetEnemy)
            {
                OnAttackEnemy(playerParamDataContainer, playerViewMonos, playerColor, args.EnemyMono);
                comboCounter.IncreaseCombo();
            }
            else
            {
                Debug.Log(
                    $"攻撃対象が{PlayerAttackModule.GetTargetEnemyEnum(playerColor)}ではないです args.EnemyMono?.EnemyEnum: {args.EnemyMono?.EnemyEnum}");
                comboCounter.ResetCombo();
            }
        }
        
        static void OnSpawnComment(
            CommentParamsServer commentParamsServer,
            CommentSpawner commentSpawner,
            ViewerNumber viewerNumber,
            OnHitEventArgs args
            )
        {
            if (args.IsTargetEnemy)
            {
                // 視聴者数が一定数以上の時、コメントを生成する
                var commentParam = commentParamsServer.GetCommentParamDependOnViewer();
                if (commentParam.viewerAmount < viewerNumber.Number)
                {
                    for (int i = 0; i < commentParam.commentAmount; i++)
                    {
                        commentSpawner.SpawnCommentByType(CommentEnum.Normal);
                    }
                }            }
            else
            {
                // 視聴者数が一定数以上の時、アンチコメントを生成する
                var commentParam = commentParamsServer.GetCommentParamDependOnViewer();
                if (commentParam.viewerAmount < viewerNumber.Number)
                {
                    for (int i = 0; i < commentParam.commentAmount; i++)
                    {
                        commentSpawner.SpawnCommentByType(CommentEnum.Spiky);
                    }
                } 
            }
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