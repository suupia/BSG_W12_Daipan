#nullable enable
using System;
using System.Collections.Generic;
using Daipan.LevelDesign.Battle.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Scripts
{
    public class PlayerSpawnPointData
    {
        public Func<List<PlayerSpawnedPosition>> GetPlayerSpawnedPointXs { get; init; } =
            () => new List<PlayerSpawnedPosition>();
        public Func<UnityEngine.Vector3> GetAttackEffectDespawnedPoint { get; init; } = () => UnityEngine.Vector3.zero;
    }
}