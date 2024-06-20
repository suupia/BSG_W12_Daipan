using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamContainer
    {
        EnemyParamWarp GetEnemyParamData(EnemyEnum enemyEnum);
    }
}