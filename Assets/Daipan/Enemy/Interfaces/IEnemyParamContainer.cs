using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyParamContainer
    {
        EnemyParamData GetEnemyParamData(EnemyEnum enemyEnum);
    }
}