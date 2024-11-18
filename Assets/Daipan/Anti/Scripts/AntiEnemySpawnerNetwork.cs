#nullable enable
using System;
using UnityEngine;
using Daipan.Enemy.Scripts;

namespace Daipan.Anti.Scripts
{
    public class AntiEnemySpawnerNetwork
    {
        public void SpawnEnemy(EnemyEnum enemyEnum)
        {
            Debug.Log($"Anti spawn {enemyEnum}");
        }
    }
}