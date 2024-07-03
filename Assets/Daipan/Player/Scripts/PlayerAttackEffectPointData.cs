#nullable enable
using Daipan.Player.MonoScripts;
using UnityEngine;

namespace Daipan.Player.Scripts
{
    public class PlayerAttackEffectPointData
    {
        readonly PlayerPositionMono _playerPositionMono;
        public PlayerAttackEffectPointData(PlayerPositionMono playerPositionMono)
        {
            _playerPositionMono = playerPositionMono;
        }

        public Vector3 GetAttackEffectSpawnedPoint() => _playerPositionMono.playerAttackEffectPosition.attackEffectSpawnedPoint.position;
        public Vector3 GetAttackEffectDespawnedPoint() => _playerPositionMono.playerAttackEffectPosition.attackEffectDespawnedPoint.position;
    }
}

