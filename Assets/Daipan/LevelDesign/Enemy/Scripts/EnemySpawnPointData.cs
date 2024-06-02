#nullable enable
using System;
using System.Collections.Generic;

namespace Daipan.LevelDesign.Enemy.Scripts
{
   
    public class EnemySpawnPointData
    {
        public Func<List<EnemySpawnedPosition>> GetEnemySpawnedPoints { get; init; } =
            () => new List<EnemySpawnedPosition>(); 
        public Func<UnityEngine.Vector3> GetEnemyDespawnedPoint { get; init; } = () => UnityEngine.Vector3.zero;
    }
 
}