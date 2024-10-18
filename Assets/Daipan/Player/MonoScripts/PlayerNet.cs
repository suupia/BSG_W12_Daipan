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
using Daipan.Core.Interfaces;
using Daipan.Enemy.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Fusion;
using R3;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerNet : NetworkBehaviour, IPlayerMono, IMonoBehaviour
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        [SerializeField] List<AbstractPlayerViewMono?> playerViewMonos = new();
        IPlayerHpParamData _playerHpParamData = null!;
        IPlayerInput _playerInput = null!;
        public Hp Hp { get; private set; } = null!;

        IPlayerOnAttacked _playerOnAttacked = null!;

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
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
            Hp = new Hp(_playerHpParamData.GetMaxHp());
            
            playerInput.SetPlayerMono(this, playerViewMonos);
            _playerInput = playerInput;
            
            playerOnAttacked.SetPlayerViews(playerViewMonos);
            _playerOnAttacked = playerOnAttacked;
        }

        public void OnAttacked(IEnemyParamData enemyParamData)
        {
            Hp = _playerOnAttacked.OnAttacked(Hp, enemyParamData);
        }
        
        public void SetHpMax()
        {
            Hp = new Hp(_playerHpParamData.GetMaxHp());
        }
        
        public int MaxHp => _playerHpParamData.GetMaxHp();
    }
}