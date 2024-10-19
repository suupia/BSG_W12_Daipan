using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using VContainer;

namespace Daipan.Enemy.Interfaces;

public interface IEnemyInitializer
{
    public void Initialize(
        PlayerHolder playerHolder
        , IEnemySpawnPoint enemySpawnPointData
        , IEnemyParamContainer enemyParamContainer
    );
}