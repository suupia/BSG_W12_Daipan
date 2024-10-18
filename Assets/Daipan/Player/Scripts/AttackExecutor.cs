#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Core.Interfaces;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class AttackExecutor : IAttackExecutor
    {
        readonly PlayerAttackEffectPointData _playerAttackEffectPointData;
        readonly PlayerAttackEffectSpawner _playerAttackEffectSpawner;

        List<AbstractPlayerViewMono?> _playerViewMonos = new();
        
        public AttackExecutor(
            PlayerAttackEffectPointData playerAttackEffectPointData,
            PlayerAttackEffectSpawner playerAttackEffectSpawner
            )
        {
            _playerAttackEffectPointData = playerAttackEffectPointData;
            _playerAttackEffectSpawner = playerAttackEffectSpawner;
        }

        public void SetPlayerViewMonos(List<AbstractPlayerViewMono?> playerViewMonos)
        {
            _playerViewMonos = playerViewMonos;
        }

        public void FireAttackEffect(IMonoBehaviour playerMono, PlayerColor playerColor)
        {
            Debug.Log($"FireAttackEffect: {playerColor}");
            var sameColorPlayerViewMono = _playerViewMonos
                .FirstOrDefault(playerViewMono => playerViewMono?.playerColor == playerColor);
            if (sameColorPlayerViewMono == null)
            {
                Debug.LogWarning($"There is no player with the same color");
                return;
            }

            var spawnPosition = _playerAttackEffectPointData.GetAttackEffectSpawnedPoint();
            _playerAttackEffectSpawner.SpawnEffect(playerMono, _playerViewMonos, playerColor, spawnPosition,
                Quaternion.identity);
        }
    }
}