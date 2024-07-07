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
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilderTutorial : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly EnemyCluster _enemyCluster;
        readonly EnemyTotemOnAttack _enemyTotemOnAttack;
        readonly WaveState _waveState;
        readonly IPlayerAntiCommentParamData _playerAntiCommentParamData;
        readonly RedEnemyTutorial _redEnemyTutorial;
        readonly SpeechEventManager _speechEventManager;

        public PlayerAttackEffectBuilderTutorial(
            IPlayerParamDataContainer playerParamDataContainer
            ,EnemyCluster enemyCluster
            ,EnemyTotemOnAttack enemyTotemOnAttack
            ,WaveState waveState
            ,IPlayerAntiCommentParamData playerAntiCommentParamData
            ,RedEnemyTutorial redEnemyTutorial
            ,SpeechEventManager speechEventManager
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _enemyCluster = enemyCluster;
            _enemyTotemOnAttack = enemyTotemOnAttack;
            _waveState = waveState;
            _playerAntiCommentParamData = playerAntiCommentParamData;
            _redEnemyTutorial = redEnemyTutorial;
            _speechEventManager = speechEventManager;
        }

        public PlayerAttackEffectMono Build(PlayerAttackEffectMono effect, PlayerMono playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            effect.SetUp(_playerParamDataContainer.GetPlayerParamData(playerColor),
                () => _enemyCluster.NearestEnemy(playerMono.transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                AttackEnemy(
                    _playerParamDataContainer
                    , playerViewMonos
                    , playerColor
                    , args.EnemyMono
                    ,_enemyTotemOnAttack
                    ,_redEnemyTutorial
                    ,_speechEventManager);
            };
            return effect;
        }



        static void AttackEnemy(IPlayerParamDataContainer playerParamDataContainer
            ,List<AbstractPlayerViewMono?> playerViewMonos
            ,PlayerColor playerColor
            ,EnemyMono? enemyMono
            ,EnemyTotemOnAttack totemOnAttack
            ,RedEnemyTutorial redEnemyTutorial
            ,SpeechEventManager speechEventManager
            )
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            if (enemyMono == null) return;


            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            if (enemyMono.EnemyEnum == EnemyEnum.Red)
            {
                Debug.Log("RedEnemyTutorial_Success");
                if(speechEventManager.CurrentEvent is RedEnemyTutorial) redEnemyTutorial.SetIsSuccess(playerColor == PlayerColor.Red);
            }
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
        
     
    }
}