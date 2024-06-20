using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.LevelDesign.Enemy.Interfaces
{
    public interface IEnemyParamContainer
    {
        EnemyParamWarp GetEnemyParamData(EnemyEnum enemyEnum);
    }
}