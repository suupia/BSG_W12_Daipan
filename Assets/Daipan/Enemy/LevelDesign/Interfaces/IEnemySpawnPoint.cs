#nullable enable
using System.Collections.Generic;
using Daipan.Enemy.Scripts;
using UnityEngine;
namespace Daipan.Enemy.LevelDesign.Interfaces
{
    public interface IEnemySpawnPoint
    {
        List<Vector3> GetEnemySpawnedPointXs();
        List<Vector3> GetEnemySpawnedPointYs();
        List<EnemyEnum> GetEnemySpawnedEnemyEnums();
        List<double> GetEnemySpawnRatios();
        Vector3 GetEnemyDespawnedPoint();
    }
}