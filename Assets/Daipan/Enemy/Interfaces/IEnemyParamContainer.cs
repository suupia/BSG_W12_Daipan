using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamContainer
    {
        IEnemyParamData GetEnemyParamData(EnemyEnum enemyEnum);
        IEnemyViewParamData GetEnemyViewParamData(EnemyEnum enemyEnum);
    }
}