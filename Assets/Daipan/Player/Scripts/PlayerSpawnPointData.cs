#nullable enable
using System;
using System.Collections.Generic;
using Daipan.LevelDesign.Battle.Scripts;
using Daipan.Player.MonoScripts;

namespace Daipan.Player.Scripts
{
    public class PlayerSpawnPointData
    {
        public Func<PlayerSpawnedPosition> GetPlayerSpawnedPointX { get; init; } =
            () => new PlayerSpawnedPosition(); 
    }
}