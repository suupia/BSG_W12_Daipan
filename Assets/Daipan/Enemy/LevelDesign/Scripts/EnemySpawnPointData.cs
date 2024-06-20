#nullable enable
using System;
using System.Collections.Generic;
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.LevelDesign.Scripts
{
   
    public class EnemySpawnPointData
    {
        public Func<List<UnityEngine.Vector3>> GetEnemySpawnedPointXs { get; init; } =
            () => new List<UnityEngine.Vector3>(); 
        public Func<List<UnityEngine.Vector3>> GetEnemySpawnedPointYs { get; init; } =
            () => new List<UnityEngine.Vector3>(); 
        public Func<List<EnemyEnum>> GetEnemySpawnedEnemyEnums { get; init; } =
            () => new List<EnemyEnum>();
        public Func<List<double>> GetEnemySpawnRatios { get; init; } =
            () => new List<double>();
        public Func<UnityEngine.Vector3> GetEnemyDespawnedPoint { get; init; } = () => UnityEngine.Vector3.zero;
    }
 
}