#nullable enable
using System;
using Daipan.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces;

public interface IEnemySpawner : IDisposable
{
    public void SpawnEnemy();
    public void SpawnEnemy(EnemyEnum enemyEnum);
    public void Dispose();
}