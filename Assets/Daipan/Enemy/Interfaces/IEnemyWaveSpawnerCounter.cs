#nullable enable
namespace Daipan.Enemy.Interfaces;

public interface IEnemyWaveSpawnerCounter
{
    public int CurrentSpawnedEnemyCount { get; }
    public int MaxSpawnedEnemyCount { get; }
    public void ResetCounter();
}