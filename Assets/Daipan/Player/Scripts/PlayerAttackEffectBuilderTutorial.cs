#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Comment.Scripts;
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
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilderTutorial : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly EnemyCluster _enemyCluster;
        readonly TutorialFacilitator _tutorialFacilitator;
        public PlayerAttackEffectBuilderTutorial(
            IPlayerParamDataContainer playerParamDataContainer
            ,EnemyCluster enemyCluster
            ,TutorialFacilitator tutorialFacilitator 
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _enemyCluster = enemyCluster;
            _tutorialFacilitator = tutorialFacilitator;
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
                    ,_tutorialFacilitator
                    );
            };
            return effect;
        }



        static void AttackEnemy(IPlayerParamDataContainer playerParamDataContainer
            ,List<AbstractPlayerViewMono?> playerViewMonos
            ,PlayerColor playerColor
            ,AbstractEnemyMono? enemyMono
            ,TutorialFacilitator tutorialFacilitator 
            )
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            if (enemyMono == null) return;

            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            if (enemyMono.EnemyEnum == EnemyEnum.Red)
            {
                // チュートリアルごとの処理 
                if (tutorialFacilitator.CurrentStep is RedEnemyTutorial redEnemyTutorial)
                {
                   if(playerColor == PlayerColor.Red) redEnemyTutorial.SetSuccess();
                }
                if(tutorialFacilitator.CurrentStep is SequentialEnemyTutorial sequentialEnemyTutorial)
                {
                    // 本来は全ての敵を倒したかどうかを判定するべきだが、最後の敵がたまたまRedなので、これで判定する
                    if(playerColor == PlayerColor.Red) sequentialEnemyTutorial.SetSuccess();
                }

            }

            if (enemyMono.EnemyEnum == EnemyEnum.Totem2)
            {
                
                if (tutorialFacilitator.CurrentStep is TotemEnemyTutorial totemEnemyTutorial)
                {
                    totemEnemyTutorial.SetSuccess();
                }
            }
            
            if (PlayerAttackModule.GetTargetEnemyEnum(playerColor).Contains(enemyMono.EnemyEnum))
            {
                // 敵を攻撃
                var playerParamData = playerParamDataContainer.GetPlayerParamData(playerColor);
                PlayerAttackModule.Attack(enemyMono, playerParamData);
                SoundManager.Instance?.PlaySe(SeEnum.Attack);
            }
            else
            {
                SoundManager.Instance?.PlaySe(SeEnum.AttackDeflect);
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