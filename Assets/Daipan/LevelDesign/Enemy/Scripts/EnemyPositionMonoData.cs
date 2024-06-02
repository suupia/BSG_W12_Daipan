#nullable enable
using System;

namespace Daipan.LevelDesign.Enemy.Scripts
{
   
    public class EnemyPositionMonoData
    {
        public Func<EnemySpawnedPosition> GetEnemySpawnedPoints { get; init; } = () => new EnemySpawnedPosition();
        public Func<UnityEngine.Transform?> GetEnemyDespawnedPoint { get; init; } = () => null;
    }
 
}