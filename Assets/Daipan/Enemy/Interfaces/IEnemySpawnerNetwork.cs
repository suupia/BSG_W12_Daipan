#nullable enable
using Daipan.Enemy.Scripts;
using Fusion;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemySpawnerNetwork
    {
        public void SpawnEnemy(EnemyEnum enemyEnum, PlayerRef playerRef);
    }
}