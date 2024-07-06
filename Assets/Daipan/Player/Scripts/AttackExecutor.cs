#nullable enable
using System.Collections.Generic;
using System.Linq;
using Daipan.Player.Interfaces;
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class AttackExecutor
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

        public void FireAttackEffect(PlayerMono playerMono, PlayerColor playerColor)
        {
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