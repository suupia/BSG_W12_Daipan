#nullable enable
using System;

namespace Daipan.Enemy.Interfaces;

public interface IEnemySpawner : IDisposable
{
    public void SpawnEnemy();
    public void Dispose();
}