#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Daipan;
using Daipan.Enemy.Interfaces;
using Daipan.Enemy.MonoScripts;
using Daipan.Enemy.Scripts;
using Daipan.Player.Interfaces;
using Daipan.Player.LevelDesign.Interfaces;
using Daipan.Player.LevelDesign.Scripts;
using Daipan.Player.Scripts;
using Fusion;
using UnityEngine;
using R3;
using VContainer;

namespace Daipan.Player.MonoScripts
{
    public sealed class PlayerAttackEffectNet : NetworkBehaviour, IPlayerAttackEffectMono
    {
        public GameObject GameObject => gameObject;
        public Transform Transform => transform;
        [SerializeField] PlayerAttackEffectViewNet? viewMono;

        public event EventHandler<OnHitEventArgs>? OnHit
        {
            add => _playerAttackTracking.OnHit += value;
            remove => _playerAttackTracking.OnHit -= value;
        }

        IPlayerAttackMove _playerAttackTracking = null!;

        [Networked]
        [OnChangedRender(nameof(OnPlayerColorChanged))]
        PlayerColor PlayerColor { get; set; } = PlayerColor.None;

        IPlayerParamDataContainer? _playerParamDataContainer;
        bool _isInitialized;

        public override void Spawned()
        {
            base.Spawned();
            Debug.Log($"PlayerAttackEffectNet Spawned");

            if (!HasStateAuthority)
            {
                var daipanScopeNet = DaipanScopeNet.BuildedContainer;
                Initialize(daipanScopeNet.Container.Resolve<IPlayerParamDataContainer>());
            }

            OnPlayerColorChanged();
        }

        public override void FixedUpdateNetwork()
        {
            base.FixedUpdateNetwork();
            _playerAttackTracking.Move(Runner.DeltaTime);
        }

        public void Initialize(IPlayerParamDataContainer playerParamDataContainer)
        {
            _playerParamDataContainer = playerParamDataContainer;
            _isInitialized = true;
        }

        public void SetUp(PlayerColor playerColor, Func<IEnemyMono?> getTargetEnemyMono)
        {
            if (!_isInitialized)
            {
                Debug.LogWarning($"PlayerAttackEffectNet is not initialized");
                return;
            }

            PlayerColor = playerColor;
            var playerParamData = _playerParamDataContainer!.GetPlayerParamData(playerColor);
            Debug.Log($"PlayerAttackEffectMono data.Enum = {playerParamData.PlayerEnum()}");
            viewMono?.SetDomain(playerParamData);
            _playerAttackTracking = new PlayerAttackLinear(
                this
                , playerParamData
                , getTargetEnemyMono
                , viewMono
            );
        }

        public void Defenced()
        {
            _playerAttackTracking.Defenced();
        }

        void OnPlayerColorChanged()
        {
            if (PlayerColor == PlayerColor.None) return;
            if (_playerParamDataContainer == null)
            {
                Debug.LogWarning($"_playerParamDataContainer is null");
                return;
            }

            var playerParamData = _playerParamDataContainer.GetPlayerParamData(PlayerColor);
            viewMono?.SetDomain(playerParamData);
        }
    }
}