#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
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
using Daipan.Tutorial.Scripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public sealed class PlayerAttackEffectBuilderTutorial : IPlayerAttackEffectBuilder
    {
        readonly IPlayerParamDataContainer _playerParamDataContainer;
        readonly EnemyCluster _enemyCluster;
        public PlayerAttackEffectBuilderTutorial(
            IPlayerParamDataContainer playerParamDataContainer
            ,EnemyCluster enemyCluster
        )
        {
            _playerParamDataContainer = playerParamDataContainer;
            _enemyCluster = enemyCluster;
        }

        public IPlayerAttackEffectMono Build(IPlayerAttackEffectMono effect, IMonoBehaviour playerMono,
            List<AbstractPlayerViewMono?> playerViewMonos, PlayerColor playerColor)
        {
            effect.Initialize(_playerParamDataContainer);
            effect.SetUp(playerColor, () => _enemyCluster.NearestEnemy(playerMono.Transform.position));
            effect.OnHit += (sender, args) =>
            {
                Debug.Log($"OnHit");
                AttackEnemy(
                    _playerParamDataContainer
                    , playerViewMonos
                    , playerColor
                    , args.EnemyMono
                    );
            };
            return effect;
        }



        static void AttackEnemy(IPlayerParamDataContainer playerParamDataContainer
            ,List<AbstractPlayerViewMono?> playerViewMonos
            ,PlayerColor playerColor
            ,IEnemyMono? enemyMono
            )
        {
            Debug.Log($"Attack enemyMono?.EnemyEnum: {enemyMono?.EnemyEnum}");
            if (enemyMono == null) return;

            Debug.Log($"EnemyType: {enemyMono.EnemyEnum}を攻撃");
            
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