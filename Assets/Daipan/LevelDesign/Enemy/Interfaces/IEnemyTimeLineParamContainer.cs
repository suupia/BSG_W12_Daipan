using Daipan.LevelDesign.Enemy.Scripts;
using Daipan.Stream.Scripts;

namespace Daipan.LevelDesign.Enemy.Interfaces
{
    public interface IEnemyTimeLineParamContainer
    {
        EnemyTimeLineParamWarp GetEnemyTimeLineParamData(StreamTimer streamTimer);
    }
}