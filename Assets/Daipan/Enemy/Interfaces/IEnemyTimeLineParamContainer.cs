using Daipan.Enemy.Scripts;
using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.Enemy.Interfaces
{
    public interface IEnemyTimeLineParamContainer
    {
        EnemyTimeLineParamData GetEnemyTimeLineParamData(StreamTimer streamTimer);
    }
}