#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Battle.scripts;
using Daipan.Battle.Scripts;
using Daipan.Enemy.Scripts;
using Daipan.InputSerial.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.Scripts;
using Daipan.Stream.Scripts;
using UnityEngine;
using VContainer;
using Daipan.Comment.Scripts;
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using R3;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerMono : MonoBehaviour
    {
        [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
        IPlayerHpParamData _playerHpParamData = null!;
        IPlayerInput _playerInput = null!;
        public Hp Hp { get; set; } = null!;

        IPlayerOnAttacked _playerOnAttacked = null!;
        
        public void Update()
        {
            _playerInput.Update();

            foreach (var playerViewMono in playerViewMonos) playerViewMono?.Idle();
        }


        [Inject]
        public void Initialize(
             IPlayerHpParamData playerHpParamData
            , IPlayerInput playerInput
            , IPlayerOnAttacked playerOnAttacked
        )
        {
            _playerHpParamData = playerHpParamData;
            playerInput.SetPlayerMono(this, playerViewMonos);
            _playerInput = playerInput;
            
            playerOnAttacked.SetPlayerViews(playerViewMonos);
            _playerOnAttacked = playerOnAttacked;
        }

        public void OnAttacked(IEnemyParamData enemyParamData)
        {
            Hp = _playerOnAttacked.OnAttacked(Hp, enemyParamData);
        }
        public int MaxHp => _playerHpParamData.GetMaxHp();
    }
}