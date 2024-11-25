#nullable enable

using Fusion;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyClusterNetwork
    {
        public PlayerRef GetPlayerRef(IEnemyMono enemy);
        public void SetPlayerRef(IEnemyMono enemy, PlayerRef playerRef);
    }
}