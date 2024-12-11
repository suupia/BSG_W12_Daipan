#nullable enable
using Daipan.Enemy.Scripts;
using Fusion;

namespace Daipan.Enemy.Interfaces
{
    public interface IAntiEnemySpawnerNetwork
    {
        public void SpawnAntiEnemy(EnemyEnum enemyEnum, PlayerRef playerRef);
    }
}