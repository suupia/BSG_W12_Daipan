using Daipan.Enemy.LevelDesign.Interfaces;
using Daipan.Player.Scripts;
using VContainer;

namespace Daipan.Enemy.Interfaces;

public interface IFinalBossInitializer
{
    public void Initialize(
        PlayerHolder playerHolder
        , IEnemySpawnPoint enemySpawnPointData
        , IFinalBossParamData finalBossParamData
        , IFinalBossViewParamData finalBossViewParamData
    );
}