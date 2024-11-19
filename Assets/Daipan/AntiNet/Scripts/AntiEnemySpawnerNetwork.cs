#nullable enable
using System;
using UnityEngine;
using Daipan.Enemy.Scripts;

namespace Daipan.AntiNet.Scripts
{
    public class AntiEnemySpawnerNetwork
    {
        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            Debug.Log($"Anti spawns {enemyEnum}");
        }
    }
}