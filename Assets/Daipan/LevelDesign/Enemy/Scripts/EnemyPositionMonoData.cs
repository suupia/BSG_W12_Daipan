#nullable enable
using System;
using System.Collections.Generic;

namespace Daipan.LevelDesign.Enemy.Scripts
{
   
    public class EnemyPositionMonoData
    {
        public Func<List<EnemySpawnedPosition>> GetEnemySpawnedPoints { get; init; } =
            () => new List<EnemySpawnedPosition>(); 
        public Func<UnityEngine.Transform?> GetEnemyDespawnedPoint { get; init; } = () => null;
    }
 
}